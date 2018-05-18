//-----------------------------------------------------------------------------------
// File Name       : KingsBeckAndCall/RiaGameManager.cs
// Author          : flanny
// Creation Date   : 18/05/2018

// Copyright © 2018 Senshu Univ. EDPS Project K.B.C>
//-----------------------------------------------------------------------------------

using UnityEngine;

namespace Ria
{
    /// <summary>
    /// アスペクト比を維持したフレキシブルなカメラ
    /// </summary>
    public class RiaFlexibleCamera
    {
        #region MEMBER
        [SerializeField, Tooltip("標準撮影縦幅")]
        private float nearestHeight = 720f;
        [SerializeField, Tooltip("遠距離撮影縦幅")]
        private float farthestHeight = 1080f;
    
        private Transform trans_ = null; // Transformのキャッシュ
        private Camera[] cameras_ = null; // Cameraのキャッシュ
        #endregion

        #region MAINFUNCTION
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="_trans">Cameraを持つtransform</param>
        /// <param name="_cameras">Cameraコンポーネント</param>
        public RiaFlexibleCamera(Transform _trans, Camera[] _cameras)
        {
            trans_ = _trans;
            cameras_ = _cameras;
        }
    
        #endregion
    }
}