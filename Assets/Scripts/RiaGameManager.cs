//-----------------------------------------------------------------------------------
// File Name       : KingsBeckAndCall/RiaGameManager.cs
// Author          : flanny
// Creation Date   : 17/05/2018

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
        private RiaSceneManager mSceneManager;
        private RiaSoundManager mSoundManager;
        private RiaTimeManager  mTimeManager;
        #endregion

        #region MAINFUNCTION
        private void Awake()
        {
            DontDestroyOnLoad(this);

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