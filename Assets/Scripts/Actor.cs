using UnityEngine;

namespace Ria
{
    public abstract class Actor
    {
        #region Static Constant Member
        private static readonly Vector3 VECTOR_ONE = Vector3.one;
        private static readonly Quaternion ROTATE_NONE = Quaternion.identity;
        #endregion

        #region Cached Member
        protected GameObject go_ = null;
        protected Transform trans_ = null;
        #endregion

        #region Member and Property
        #endregion

        /// <summary>
        /// 生成(GameObjectを与えるver)
        /// </summary>
        /// <param name="_gameObject">GameObject</param>
        /// <param name="_localPosition">localPosition</param>
        public Actor(GameObject _gameObject, Vector3 _localPosition)
        {
            this.go_ = _gameObject;
            this.trans_ = go_.transform;
            this.trans_.localRotation = ROTATE_NONE;
            this.trans_.localScale    = VECTOR_ONE;
            this.trans_.localPosition = _localPosition;
        }

        /// <summary>
        /// 生成(GameObjectを生成するver)
        /// </summary>
        /// <param name="_name">GameObjectの名前</param>
        /// <param name="_localPosition">localPosition</param>
        public Actor(string _name, Vector3 _localPosition)
        {
            this.go_ = new GameObject(_name);
            this.trans_ = go_.transform;
            this.trans_.localRotation = ROTATE_NONE;
            this.trans_.localScale = VECTOR_ONE;
            this.trans_.localPosition = _localPosition;
        }

        // 派生クラスでの固有定義
        public abstract void OnUpdate();
    }
}