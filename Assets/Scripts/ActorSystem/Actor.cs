using UnityEngine;

namespace Ria
{
    public abstract class Actor
    {
        private static readonly Vector3 VECTOR_ONE = Vector3.one;
        private static readonly Quaternion ROTATE_NONE = Quaternion.identity;

        protected GameObject go_ = null;
        protected Transform trans_ = null;

        protected bool mIsActive;

        public Actor()
        {
            go_ = null;
            trans_ = null;
            mIsActive = false;
        }

        public Actor(GameObject _gameObject, Vector3 _localPosition)
        {
            this.go_ = _gameObject;
            this.trans_ = go_.transform;
            this.trans_.localPosition = _localPosition;
        }

        public Actor(string _name, Vector3 _localPosition)
        {
            this.go_ = new GameObject(_name);
            this.trans_ = go_.transform;
            this.trans_.localRotation = ROTATE_NONE;
            this.trans_.localScale = VECTOR_ONE;
            this.trans_.localPosition = _localPosition;
        }

        public void Create()
        {
            OnCreate();
            mIsActive = true;
        }

        public void Create(GameObject _go, Transform _trans)
        {
            this.go_ = _go;
            this.trans_ = _trans;

            OnCreate();
            mIsActive = true;
        }

        public void Run()
        {
            if (!mIsActive) { return; }

            OnRun();
        }

        public void Stop()
        {
            mIsActive = false;
        }

        protected abstract void OnCreate();
        protected abstract void OnRun();
    }

}