using UnityEngine;
namespace UnityManiax
{

    // 左方向直進弾
    public class MissileLeft : Missile2D
    {
        public override void Ignition()
        {
            this.direct = Vector3.left;
        }
    }
}