using UnityEngine;

namespace Ria
{
    public abstract class CachedActor
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
        public UNIQUEID uniqueId;
        private bool reqStop = false;
        public bool Alive { get; private set; }
        #endregion

        /// <summary>
        /// 生成(GameObjectを与えるver)
        /// </summary>
        /// <param name="_gameObject">GameObject</param>
        public void Create(GameObject _gameObject, ScriptableObject _scriptable, UNIQUEID _uniqueId)
        {
            this.go_ = _gameObject;
            this.trans_ = go_.transform;
            this.uniqueId = _uniqueId;
            this.go_.SetActive(false);
            OnCreate(_gameObject, _scriptable, _uniqueId);
        }

        /// <summary>
        /// 生成(GameObjectを生成して返すver)
        /// </summary>
        /// <param name="_name">GameObjectの名前</param>
        public GameObject Create(string _name, ScriptableObject _scriptable, UNIQUEID _uniqueId)
        {
            this.go_ = new GameObject(_name);
            this.trans_ = go_.transform;
            this.uniqueId = _uniqueId;
            this.go_.SetActive(false);
            OnCreate(_name, _scriptable, _uniqueId);

            return go_;
        }

        /// <summary>
        /// 廃棄処理
        /// </summary>
        public void Release()
        {
            this.OnRelease();
        }

        /// <summary>
        /// 起動処理
        /// </summary>
        /// <param name="_localPosition"></param>
        public void WakeUp(Vector3 _localPosition)
        {
            this.go_.SetActive(true);
            this.trans_.localRotation = ROTATE_NONE;
            this.trans_.localScale = VECTOR_ONE;
            this.trans_.localPosition = _localPosition;
            this.OnAwake();
        }

        /// <summary>
        /// 停止処理
        /// </summary>
        public void Sleep()
        {
            this.go_.SetActive(false);
            this.OnSleep();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="elapsedTime">経過時間</param>
        public bool Run(float elapsedTime)
        {
            if (this.reqStop) { return false; }
            this.Alive = this.OnRun(elapsedTime);
            return this.Alive;
        }

        /// <summary>
        /// 停止命令
        /// </summary>
        public virtual void Stop()
        {
            this.reqStop = true;
            this.Alive = false;
        }

        // 派生クラスでの固有定義
        protected abstract void OnCreate(GameObject _go, ScriptableObject _scriptable, UNIQUEID _uniqueId);
        protected abstract void OnCreate(string _name, ScriptableObject _scriptable, UNIQUEID _uniqueId);
        public abstract void OnUpdate();
        protected abstract void OnRelease();
        protected abstract void OnAwake();
        protected abstract void OnSleep();
        protected abstract bool OnRun(float elapsedTime);
    }
}