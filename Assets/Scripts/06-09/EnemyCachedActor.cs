using UnityEngine;

namespace old_0609
{
    using UnityManiax;

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

        protected override void OnRelease()
        {

        }

        protected override void OnAwake()
        {
        }

        protected override void OnSleep()
        {

        }

        protected override bool OnRun(float _elapsedTime)
        {
            this.trans_.position += Vector3.down * mScriptable.speed;

            if (this.trans_.position.y < -540 || this.trans_.position.y > 540)
            {
                return false;
            }

            Debug.Log(this.go_.name);
            return true;
        }
    }
}