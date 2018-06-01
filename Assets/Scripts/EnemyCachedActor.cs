using UnityEngine;

namespace Ria
{
    public class EnemyCachedActor : CachedActor
    {
        #region Member
        EnemyScriptableObject mScriptable;
        private SpriteRenderer sr_ = null;
        #endregion

        protected override void OnCreate(GameObject _go, ScriptableObject _scriptable, UNIQUEID _uniqueId)
        {
            this.mScriptable = _scriptable as EnemyScriptableObject;
            this.sr_ = go_.AddComponent<SpriteRenderer>();
            this.sr_.sprite = mScriptable.sprite;
        }

        protected override void OnCreate(string _name, ScriptableObject _scriptable, UNIQUEID _uniqueId)
        {
            this.mScriptable = _scriptable as EnemyScriptableObject;
        }
        
        public override void OnUpdate()
        {
        }

        protected override void OnRelease()
        {

        }

        protected override void OnAwake()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override bool OnRun(float elapsedTime)
        {
            this.trans_.position += Vector3.down * mScriptable.speed;
            return true; //elapsedTime <= time;
        }
    }
}