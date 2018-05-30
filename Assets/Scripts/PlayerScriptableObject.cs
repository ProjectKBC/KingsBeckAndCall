using UnityEngine;
using UnityEditor;

public class PlayerScriptableObject : ScriptableObject
{
    #region Member
    [SerializeField, Tooltip("名前")]
    public string playerName;
    [SerializeField, Tooltip("画像")]
    public Sprite sprite;
    [SerializeField, Range(1, 10), Tooltip("攻撃力")]
    public int attack = 1;
    [SerializeField, Range(1, 10), Tooltip("速度")]
    public int speed = 1;
    #endregion

    [MenuItem("Example/Create PlayerScriptableObject Instance")]
    static void CreateExampleAssetInstance()
    {
        var exampleAsset = CreateInstance<PlayerScriptableObject>();

        AssetDatabase.CreateAsset(exampleAsset, "Assets/ScriptableObject/PlayerScriptableObject.asset");
        AssetDatabase.Refresh();
    }
}
