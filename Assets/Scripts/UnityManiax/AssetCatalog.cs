using UnityEngine;
using System.Collections.Generic;

namespace UnityManiax
{
    /// <summary>
    /// Asset 列挙
    /// </summary>
    public class AssetCatalog : ScriptableObject
    {
        /// <summary>
        /// シリアライズ用クラス
        /// </summary>
        [System.Serializable]
        public sealed class SettingParam
        {
            public SettingParam(string key, Object origin, int genCount)
            {
                this.key = key;
                this.origin = origin;
                this.genCount = genCount;
            }
            public string key = null;    // ID 名
            public Object origin = null; // オリジナルObject
            public int genCount = 1;     // 生成数
        }

        [SerializeField]
        private SettingParam[] serializedParams = new SettingParam[0];
 
        /// <summary>
        /// Object の取得
        /// </summary>
        /// <param name="type">種類</param>
        /// <returns>Object</returns>
        public Object GetObject(int type)
        {
            return this.serializedParams[type].origin;
        }

        /// <summary>
        /// Object の生成数取得
        /// </summary>
        /// <param name="type">種類</param>
        /// <returns></returns>
        public int GetGenerateCount(int type)
        {
            return this.serializedParams[type].genCount;
        }
        
        /// <summary>
        /// 種類数の取得
        /// </summary>
        /// <returns>種類数</returns>
        public int GetCatalogCount()
        {
            return this.serializedParams.Length;
        }

#if UNITY_EDITOR

        /// <summary>
        /// 編集用データ
        /// </summary>
        [System.NonSerialized]
        public Dictionary<string, SettingParam> editParamDic = new Dictionary<string, SettingParam>();

        /// <summary>
        /// 編集用Dictionary の作成
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        public bool CreateDictionary<U>() where U : struct
        {
            System.Array enumArray = System.Enum.GetValues(typeof(U));
            int objectCount = enumArray.Length;
            this.editParamDic.Clear();

            // 保存されている配列のリスト化
            List<SettingParam> serializedList = new List<SettingParam>(this.serializedParams);
            bool changed = false;

            // シリアライズデータからEDITOR 用のテーブルを作成
            for (int i = 0; i < serializedList.Count; ++i)
            {
                SettingParam p = serializedList[i];

                // 既に値が存在するかチェック
                bool find = false;

                for (int j = 0; j < objectCount; ++j)
                {
                    if (p.key == enumArray.GetValue(j).ToString())
                    {
                        find = true;
                        break;
                    }
                }

                // 存在しない、または重複しているなら削除
                if (!find || this.editParamDic.ContainsKey(p.key))
                {
                    serializedList.Remove(p);
                    --i;
                    changed = true;
                    continue;
                }
                this.editParamDic.Add(p.key, p);
            }

            for (int i = 0; i < objectCount; ++i)
            {
                string key = enumArray.GetValue(i).ToString();

                // 不足分の項目を作成
                if (!this.editParamDic.ContainsKey(key))
                {
                    SettingParam param = new SettingParam(key, null, 1);
                    this.editParamDic.Add(key, param);
                    changed = true;
                }
            }

            // 削除対応したものを更新
            this.serializedParams = serializedList.ToArray();
            return changed;
        }

        /// <summary>
        /// シリアライズデータの更新
        /// </summary>
        /// <typeparam name="U"></typeparam>
        public void UpdateSerializedList<U>() where U : struct
        {
            System.Array enumArray = System.Enum.GetValues(typeof(U));
            int objectCount = enumArray.Length;
            this.serializedParams = new SettingParam[objectCount];
            for (int i = 0; i < objectCount; ++i)
            {
                string key = enumArray.GetValue(i).ToString();
                SettingParam param = null;
                if (!this.editParamDic.TryGetValue(key, out param))
                    param = new AssetCatalog.SettingParam(key, null, 1);
                this.serializedParams[i] = param;
            }
        }
#endif
    }
}