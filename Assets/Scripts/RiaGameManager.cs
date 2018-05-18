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
    /// 
    /// </summary>
    public sealed class RiaGameManager : MonoBehaviour
    {
        #region MEMBER
        [SerializeField, Tooltip("Camera")]
        private GameObject mCameraObj;

        private RiaCameraManager mCameraManager;
        private RiaSceneManager  mSceneManager;
        private RiaSoundManager  mSoundManager;
        private RiaTimeManager   mTimeManager;
        #endregion

        #region MAINFUNCTION
        private void Awake()
        {
            DontDestroyOnLoad(this);

            mCameraManager = new RiaCameraManager();
            mSceneManager = new RiaSceneManager();
            mSoundManager = new RiaSoundManager();
            mTimeManager  = new RiaTimeManager();
        }
        
        private void Update()
        {
            mSceneManager.OnUpdate();
            mSoundManager.OnUpdate();
            mTimeManager.OnUpdate();
        }
        #endregion
    }

}