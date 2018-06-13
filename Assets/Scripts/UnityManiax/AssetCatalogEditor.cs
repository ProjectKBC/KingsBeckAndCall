using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace UnityManiax
{
    /// <summary>
    /// Object 列挙一覧
    /// </summary>
    /// <typeparam name="T">列挙体</typeparam>
    /// <typeparam name="U">継承インターフェース</typeparam>
    public class AssetCatalogEditor<T, U> : Editor where T : Object
    where U : struct
    {
        // 有効時
        void OnEnable()
        {
            AssetCatalog t = this.target as AssetCatalog;
            if (t != null)
            {
                bool changed = t.CreateDictionary<U>();
                // enum が増減していたら変更されてくる
                if (changed)
                {
                    t.UpdateSerializedList<U>();
                    EditorUtility.SetDirty(t);
                }
            }
        }

        // Inspector 表示
        public override void OnInspectorGUI()
        {
            AssetCatalog t = this.target as AssetCatalog;
            Dictionary<string, AssetCatalog.SettingParam> dic = t.editParamDic;
            GUILayout.Space(5f);
            // Prefab 設定
            System.Array enumArray = System.Enum.GetValues(typeof(U));
            int objectCount = enumArray.Length;
            for (int i = 0; i < objectCount; ++i)
            {
                string key = enumArray.GetValue(i).ToString();
                AssetCatalog.SettingParam param = dic[key];
                GUILayout.BeginHorizontal();
                GUIContent content = new GUIContent(i.ToString("D3") + " " + key,
                "Asset 指定");
                dic[key].origin = EditorGUILayout.ObjectField(content,
                param.origin,
                typeof(T),
                false,
                GUILayout.MinWidth(250f)) as T;
                dic[key].genCount = EditorGUILayout.IntField(param.genCount,
                GUILayout.MaxWidth(100f));
                GUILayout.EndHorizontal();
            }
            if (GUI.changed || (objectCount != t.GetCatalogCount()))
            {
                t.UpdateSerializedList<U>();
                EditorUtility.SetDirty(t);
            }
        }
    }
}