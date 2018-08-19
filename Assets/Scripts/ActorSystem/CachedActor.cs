using UnityEngine;
using UnityManiax;

namespace Ria
{
    public abstract class CachedActor
    {
        private static readonly Vector3 VECTOR_ONE = Vector3.one;
        private static readonly Quaternion ROTATE_NONE = Quaternion.identity;

        protected GameObject go_ = null;
        protected Transform trans_ = null;

        public UNIQUEID uniqueId;
        private bool reqStop = false;
        public bool Alive { get; private set; }

        public void Create(GameObject _gameObject, UNIQUEID _uniqueId, ScriptableObject _scriptable = null)
        {
            this.go_ = _gameObject;
            this.trans_ = go_.transform;
            this.uniqueId = _uniqueId;
            this.go_.SetActive(false);
            OnCreate(_gameObject, _scriptable, _uniqueId);
        }

        public GameObject Create(string _name, UNIQUEID _uniqueId, ScriptableObject _scriptable = null)
        {
            this.go_ = new GameObject(_name);
            this.trans_ = go_.transform;
            this.uniqueId = _uniqueId;
            this.go_.SetActive(false);
            OnCreate(_name, _scriptable, _uniqueId);

            return go_;
        }

        public void Release()
        {
            this.OnRelease();
        }

        public void WakeUp(Vector3 _localPosition)
        {
            this.go_.SetActive(true);
            this.trans_.localRotation = ROTATE_NONE;
            this.trans_.localScale    = VECTOR_ONE;
            this.trans_.localPosition = _localPosition;
            this.OnAwake();
        }

        public void Sleep()
        {
            this.go_.SetActive(false);
            this.OnSleep();
        }

        public bool Run(float elapsedTime)
        {
            if (this.reqStop) { return false; }
            this.Alive = this.OnRun(elapsedTime);
            return this.Alive;
        }

        public virtual void Stop()
        {
            this.reqStop = true;
            this.Alive = false;
        }

        protected abstract void OnCreate(GameObject _go, ScriptableObject _scriptable, UNIQUEID _uniqueId);
        protected abstract void OnCreate(string _name, ScriptableObject _scriptable, UNIQUEID _uniqueId);
        protected abstract void OnRelease();
        protected abstract void OnAwake();
        protected abstract void OnSleep();
        protected abstract bool OnRun(float elapsedTime);
    }
}
