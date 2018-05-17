using UnityEngine;

namespace Ria
{
    /// <summary>
    /// アスペクト比を維持したフレキシブルなカメラ
    /// </summary>
    public class RiaFlexibleCamera : MonoBehaviour
    {
        #region MEMBER
        [SerializeField, Tooltip("標準撮影縦幅")]
        private float nearestHeight = 720f;
        [SerializeField, Tooltip("遠距離撮影縦幅")]
        private float farthestHeight = 1080f;

        private Transform _trans = null;  // Transformのキャッシュ
        private Camera[] _cameras = null; // Cameraのキャッシュ
        #endregion

        #region MAINFUNCTION
        #endregion
    }
}