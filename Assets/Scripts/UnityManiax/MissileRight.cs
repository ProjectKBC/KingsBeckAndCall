using UnityEngine;
namespace UnityManiax
{

    // 右方向直進弾
    public class MissileRight : Missile2D
    {
        public override void Ignition()
        {
            this.direct = Vector3.right;
        }
    }
}