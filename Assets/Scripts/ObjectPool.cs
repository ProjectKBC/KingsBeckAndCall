using UnityEngine;

namespace Ria
{
    public class ObjectPool<T> where T : CachedActor, new()
    {
        #region Member
        /// <summary>
        /// 各オブジェクトの共有設定
        /// </summary>
        private struct ObjectParam
        {
            public CachedActor actor;
            /// <summary>
            /// 元のprefab
            /// </summary>
            public ScriptableObject scriptable;
            /// <summary>
            /// 生成した親ノード
            /// </summary>
            public Transform root;
            /// <summary>
            /// 空きオブジェクトバッファ
            /// </summary>
            public T[] pool;
            /// <summary>
            /// 空きオブジェクトインデックス
            /// </summary>
            public int freeIndex;
            /// <summary>
            /// 生成限界数
            /// </summary>
            public int genMax;
            /// <summary>
            /// 生成した数
            /// </summary>
            public int genCount;
        }

        /// <summary>
        /// オブジェクトのカテゴリ
        /// </summary>
        private int category = 0;
        /// <summary>
        /// オブジェクト種類数
        /// </summary>
        private int typeCount = 0;
        /// <summary>
        /// オブジェクト情報
        /// </summary>
        private ObjectParam[] objParams = null;
        /// <summary>
        /// 全オブジェクトリスト
        /// </summary>
        private T[][] objList = null;
        /// <summary>
        /// 稼動オブジェクトタスク
        /// </summary>
        private TaskSystem<T> activeObjTask = null;
        /// <summary>
        /// 実行オブジェクト数
        /// </summary>
        private int orderCount = 0;
        /// <summary>
        /// procHandler 経過時間
        /// </summary>
        private float advanceTime = 0f;
        /// <summary>
        /// 稼働オブジェクトタスク数
        /// </summary>
        public int ActCount { get { return this.activeObjTask.ActCount; } }
        #endregion

        #region Cached Member 
        private OrderHandler<T> procHandler = null;
        private OrderHandler<T> clearHandler = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ObjectPool()
        {
            // デリゲートキャッシュ
            this.procHandler = new OrderHandler<T>((obj, no) => {
                float elapsedTime = this.advanceTime;
                // 新規追加されたものは経過時間0sec.
                if (no >= this.orderCount)
                    elapsedTime = 0f;
                if (!obj.Run(elapsedTime))
                {
                    this.Sleep(obj);
                    return false;
                }
                return true;
            });
            this.clearHandler = new OrderHandler<T>((obj, no) => {
                this.Sleep(obj);
                return false;
            });
        }
        
        /// <summary>
        /// 初期化
        /// /// </summary>
        /// <param name="category">オブジェクトのカテゴリ指定</param>
        /// <param name="prefabs">複製するPrefab リスト</param>
        /// <param name="caps">複製限界数リスト</param>
        public void Initialize(int category, ScriptableObject[] scriptables, int[] caps)
        {
            // 初期化エラーチェック
            Debug.Assert(scriptables != null && scriptables.Length > 0);
            Debug.Assert(caps != null || caps.Length == scriptables.Length);
            this.category = category;
            this.typeCount = scriptables.Length;
            this.objList = new T[this.typeCount][];
            this.objParams = new ObjectParam[this.typeCount];


            // scriptable 読込
            int capacity = 0;
            for (int type = 0; type < this.typeCount; ++type)
            {
                int genMax = caps[type];
                if (genMax == 0)
                    continue;
                this.objList[type] = new T[genMax];
                this.objParams[type].pool = new T[genMax];
                this.objParams[type].freeIndex = -1;
                ScriptableObject scriptable = scriptables[type];

                // 開発用にまだPrefab が用意されていない場合を考慮してnull を許容する
                if (scriptables == null) { continue; }

                this.objParams[type].genMax = genMax;
                this.objParams[type].genCount = 0;
                this.objParams[type].scriptable = scriptable;
                // 親ノード作成
                GameObject typeGo = new GameObject(scriptable.name);
                typeGo.isStatic = true;
                Transform typeRoot = typeGo.transform;
                this.objParams[type].root = typeRoot;
                // MEMO: シーン切替で自動で削除させない
                Object.DontDestroyOnLoad(typeGo);
                capacity += genMax;
            }
            this.activeObjTask = new TaskSystem<T>(capacity);
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Final()
        {
            for (int type = 0; type < this.typeCount; ++type)
            {
                if (this.objList[type] == null)
                    continue;
                int count = this.objList[type].Length;
                for (int index = 0; index < count; ++index)
                {
                    if (this.objList[type][index] == null)
                        break;
                    this.objList[type][index].Release();
                }
                Object.Destroy(this.objParams[type].root);
            }
            this.category = 0;
            this.typeCount = 0;
            this.objList = null;
            this.objParams = null;
            this.activeObjTask = null;
        }

        /// <summary>
        /// 全オブジェクトの生成
        /// 生成数が増えると時間がかかりがちなのでInitializeと分ける
        /// </summary>
        public void Generate()
        {
            for (int type = 0; type < this.typeCount; ++type)
            {
                int genLimit = this.objParams[type].genMax -
                this.objParams[type].genCount;
                for (int index = 0; index < genLimit; ++index)
                {
                    if (this.objList[type][index] != null)
                        continue;
                    T obj = this.GenerateObject(type);
                    int freeIndex = ++this.objParams[type].freeIndex;
                    this.objParams[type].pool[freeIndex] = obj;
                }
            }
        }
        
        /// <summary>
        /// オブジェクトの生成
        /// </summary>
        /// <param name="type">オブジェクトの種類</param>
        /// <returns></returns>
        private T GenerateObject(int type)
        {
            int index = this.objParams[type].genCount;
            ScriptableObject scriptable = this.objParams[type].scriptable;
            Transform root = this.objParams[type].root;
            GameObject go = new GameObject();
            go.transform.parent = root;
            
#if UNITY_EDITOR
            go.name = string.Format(this.objParams[type].scriptable.name + "{0:D2}",
            this.objParams[type].genCount);
#endif
            T obj = new T();
            
            // ユニークID を割り振り
            obj.Create(go, scriptable, UNIQUEID.Create(UNIQUEID.CATEGORYBIT(this.category) | UNIQUEID.TYPEBIT(type) | UNIQUEID.INDEXBIT(index)));
            
            this.objList[type][index] = obj;
            ++this.objParams[type].genCount;
            return obj;
        }
        
        /// <summary>
        /// フレームの頭で呼ばれる処理
        /// </summary>
        public void FrameTop()
        {
            // 更新オブジェクト数の更新
            this.orderCount = this.activeObjTask.ActCount;
        }
        
        /// <summary>
        /// 定期更新
        /// </summary>
        /// <param name="elapsedTime">経過時間</param>
        public void Proc(float elapsedTime)
        {
            this.advanceTime = elapsedTime;
            if (this.activeObjTask.ActCount > 0)
            {
                this.activeObjTask.Order(this.procHandler);
                this.orderCount = this.activeObjTask.ActCount;
            }
        }
        
        /// <summary>
        /// 種類別有効数取得
        /// </summary>
        /// <param name="type">種類</param>
        /// <returns></returns>
        public int GetActiveCount(int type)
        {
            return this.objParams[type].genCount -
            (this.objParams[type].freeIndex + 1);
        }

        /// <summary>
        /// 全消去
        /// </summary>
        public void Clear()
        {
            this.activeObjTask.Order(this.clearHandler);
        }
 
        /// <summary>
        /// オブジェクト呼び出し
        /// </summary>
        /// <param name="type">種類</param>
        /// <param name="localPosition">生成座標</param>
        /// <param name="obj">生成したオブジェクト</param>
        /// <returns>呼び出しに成功</returns>
        public bool AwakeObject(int type, Vector3 localPosition, out T obj)
        {
            if (this.PickOutObject(type, out obj))
            {
                int no = this.activeObjTask.ActCount - 1;
                obj.WakeUp(localPosition);
                return true;
            }
            return false;
        }

        /// <summary>
        /// オブジェクト取得
        /// </summary>
        /// <param name="unique">ユニークID</param>
        /// <param name="obj">対象オブジェクト</param>
        /// <returns>ID が一致したか（異なる場合は既に一度回収されている）</returns>
        public bool GetObject(UNIQUEID unique, out T obj)
        {
            // 関係のないユニークID
            if (this.category != unique.category)
            {
                obj = null;
                return false;
            }
            obj = this.objList[unique.type][unique.index];
            if (!obj.Alive) { return false; }

            // フラッシュID が更新されていれば別人
            return (obj.uniqueId == unique);
        }

        /// <summary>
        /// オブジェクト取り出し
        /// /// </summary>
        /// <param name="type">種類</param>
        /// <param name="obj">取り出したオブジェクト</param>
        /// <returns></returns>
        private bool PickOutObject(int type, out T obj)
        {
            obj = null;
            // 空きオブジェクトを取り出す
            if (this.objParams[type].freeIndex >= 0)
            {
                obj = this.objParams[type].pool[this.objParams[type].freeIndex];
                --this.objParams[type].freeIndex;
            }
            else
            {
                return false;
            }
            this.activeObjTask.Attach(obj);
            obj.uniqueId.Update();
            return true;
        }

        /// <summary>
        /// 稼動終了処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        private void Sleep(T obj)
        {
            int type = obj.uniqueId.type;
            ++this.objParams[type].freeIndex;
            this.objParams[type].pool[this.objParams[type].freeIndex] = obj;
            obj.Sleep();
        }
    }
}
