using UnityEngine;
using UnityEditor;

namespace Ria
{
    public abstract class ActorScriptableObject : ScriptableObject
    {
        [SerializeField, Tooltip("当たり判定：モード")]
        public COLLIDER_MODE colliderMode;
        [SerializeField, Tooltip("当たり判定：半径")]
        public float colliderRadius;
        [SerializeField, Tooltip("当たり判定：デバッグモード")]
        public bool colliderIsDebugMode;

    }
}