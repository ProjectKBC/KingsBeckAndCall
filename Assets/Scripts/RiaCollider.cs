using UnityEngine;

namespace Ria
{
    public enum COLLIDER_MODE
    {
        ELLIPSE,
    }

    public sealed class RiaCollider
    {
        private GameObject gameObject = null;
        private Transform trans_ = null;
        private ActorScriptableObject scriptableObject = null;
        private SpriteRenderer renderer = null;
        //private Material mActiveDebugMaterial = null;
        //private Material mSleepDebugMaterial = null;
        //private bool isDebugMode = false;

        public RiaCollider(GameObject _go, Transform _trans, ActorScriptableObject _scriptableObject)
        {
            this.gameObject = _go;
            this.trans_ = _trans;
            this.scriptableObject = _scriptableObject;
            //this.isDebugMode = this.scriptableObject.colliderIsDebugMode;

            //this.mActiveDebugMaterial = Resources.Load("Materials/RiaColliderDebugMode_Active", typeof(Material)) as Material;
            //this.mSleepDebugMaterial  = Resources.Load("Materials/RiaColliderDebugMode_Sleep", typeof(Material)) as Material;

            //Debug.Log(mActiveDebugMaterial);

            //this.renderer = gameObject.GetComponent<SpriteRenderer>();
            //this.renderer.material = mActiveDebugMaterial;
            //this.renderer.material.SetFloat("_radius", scriptableObject.colliderRadius);
        }

        public bool IsInColliderArea(RiaCollider _target)
        {
            float distanceX = _target.trans_.position.x - this.trans_.position.x;
            float distanceY = _target.trans_.position.y - this.trans_.position.y;
            float sumRadius = _target.scriptableObject.colliderRadius + this.scriptableObject.colliderRadius;
            return distanceX * distanceX + distanceY * distanceY <= sumRadius * sumRadius;
        }

        public void Run()
        {
        }
    }
}