using UnityEngine;
using UnityEditor;

namespace old_0609
{
    [CreateAssetMenu(menuName = "ScriptableObject/Player")]
    public sealed class PlayerScriptableObject : ScriptableObject
    {
        [SerializeField, Tooltip("名前")]
        public string playerName;
        [SerializeField, Tooltip("画像")]
        public Sprite sprite;
        [SerializeField, Range(1, 10), Tooltip("攻撃力")]
        public int attack = 1;
        [SerializeField, Range(1, 10), Tooltip("速度")]
        public int speed = 1;
    }
}