using UnityEngine;
using UnityEditor;

namespace Ria
{
    public class EnemyScriptableObject : ScriptableObject
    {
        #region Member
        [SerializeField, Tooltip("名前")]
        public string playerName;
        [SerializeField, Tooltip("画像")]
        public Sprite sprite;
        [SerializeField, Range(1, 10), Tooltip("攻撃力")]
        public int attack = 1;
        [SerializeField, Range(1, 50), Tooltip("速度")]
        public int speed = 1;
        #endregion

        [MenuItem("Example/Create EnemyScriptableObject Instance")]
        static void CreateExampleAssetInstance()
        {
            var exampleAsset = CreateInstance<EnemyScriptableObject>();

            AssetDatabase.CreateAsset(exampleAsset, "Assets/ScriptableObject/EnemyScriptableObject.asset");
            AssetDatabase.Refresh();
        }
    }
}