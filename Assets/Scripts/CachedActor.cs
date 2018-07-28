using UnityEngine;
using UnityManiax;

namespace Ria
{
    public class CachedActor
    {
        public UNIQUEID uniqueId;
        protected bool isActive { get; set; }
        protected GameObject go_ = null;
        protected Transform trans_ = null;
    }
}
