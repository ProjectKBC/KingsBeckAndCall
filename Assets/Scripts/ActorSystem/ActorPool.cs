using UnityEngine;
using UnityManiax;
using UnityManiax.TaskList;

namespace Ria
{
    using UnityManiax;
    using UnityManiax.TaskList;

    public class ActorPool<T> where T : CachedActor, new()
    {
        private struct ObjectParam
        {
            public CachedActor actor;
            public ScriptableObject scriptable;

            // 生成した親ノード
            public Transform root;
            // 空きオブジェクトバッファ
            public T[] pool;
            // 空きオブジェクトインデックス
            public int freeIndex;
            // 生成限界数
            public int genMax;
            // 生成した数
            public int genCount;
        }
        // オブジェクトのカテゴリ
        private int category = 0;
        // オブジェクト種類数
        private int typeCount = 0;
        // オブジェクト情報
        private ObjectParam[] objParams = null;
        // 全オブジェクトリスト
        private T[][] objList = null;
        // 稼動オブジェクトタスク
        private TaskSystem<T> activeObjTask = null;
        // 実行オブジェクト数
        private int orderCount = 0;
        // procHandler 経過時間
        private float advanceTime = 0f;
        // 稼働オブジェクトタスク数
        public int ActCount { get { return this.activeObjTask.count; } }

        private OrderHandler<T> procHandler = null;
        private OrderHandler<T> clearHandler = null;

        public ActorPool()
        {
            // デリゲートキャッシュ
            this.procHandler = new OrderHandler<T>((obj, no) =>
            {
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
            this.clearHandler = new OrderHandler<T>((obj, no) =>
            {
                this.Sleep(obj);
                return false;
            });
        }

        // category オブジェクトのカテゴリ指定
        // prefabs  複製するPrefab リスト
        // caps     複製限界数リスト
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

        // type オブジェクトの種類</param>
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

        // フレームの頭で呼ばれる処理
        public void FrameTop()
        {
            // 更新オブジェクト数の更新
            this.orderCount = this.activeObjTask.count;
        }

        // name elapsedTime
        public void Proc(float elapsedTime)
        {
            this.advanceTime = elapsedTime;
            if (this.activeObjTask.count > 0)
            {
                this.activeObjTask.Order(this.procHandler);
                this.orderCount = this.activeObjTask.count;
            }
        }

        // type 種類
        public int GetActiveCount(int type)
        {
            return this.objParams[type].genCount -
            (this.objParams[type].freeIndex + 1);
        }
        
        public void Clear()
        {
            this.activeObjTask.Order(this.clearHandler);
        }

        // type          種類
        // localPosition 生成座標
        // obj           生成したオブジェクト
        public bool AwakeObject(int type, Vector3 localPosition, out T obj)
        {
            if (this.PickOutObject(type, out obj))
            {
                int no = this.activeObjTask.count - 1;
                obj.WakeUp(localPosition);
                return true;
            }
            return false;
        }
        
        // unique ユニークID
        // obj    対象オブジェクト
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

        // type 種類
        // obj  取り出したオブジェクト
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

        private void Sleep(T obj)
        {
            int type = obj.uniqueId.type;
            ++this.objParams[type].freeIndex;
            this.objParams[type].pool[this.objParams[type].freeIndex] = obj;
            obj.Sleep();
        }
    }
}