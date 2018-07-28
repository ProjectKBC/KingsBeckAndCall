using UnityEngine;

namespace Ria
{
    public abstract class ChildManager
    {
        protected ScriptableObject scriptable = null;

        public void Init()
        {
            OnInit();
        }

        public void Init(ScriptableObject _scriptable)
        {
            scriptable = _scriptable;
        
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