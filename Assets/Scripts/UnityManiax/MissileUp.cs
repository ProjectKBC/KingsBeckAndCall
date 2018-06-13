using UnityEngine;

namespace UnityManiax
{

    // 上方向直進弾
    public class MissileUp : Missile2D
    {
        public override void Ignition()
        {
            this.direct = Vector3.up;
        }
    }
}