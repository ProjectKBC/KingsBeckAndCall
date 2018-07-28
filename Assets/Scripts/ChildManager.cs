using UnityEngine;

namespace Ria
{
    public abstract class ChildManager
    {
        protected ScriptableObject scriptable = null;
        protected GameObject gameObject = null;

        public void Init(GameObject _gameObject)
        {
            gameObject = _gameObject;
            OnInit();
        }

        public void Init(GameObject _gameObject, ScriptableObject _scriptable)
        {
            scriptable = _scriptable;
            gameObject = _gameObject;

            OnInit();
        }

        public void Run()
        {
            OnRun();
        }

        protected abstract void OnInit();
        protected abstract void OnRun();
    }
}