//-----------------------------------------------------------------------------------
// File Name       : KingsBeckAndCall/RiaGameManager.cs
// Author          : flanny
// Creation Date   : 18/05/2018

// Copyright © 2018 Senshu Univ. EDPS Project K.B.C>
//-----------------------------------------------------------------------------------

using UnityEngine;

namespace Ria
{
    public class RiaCameraManager : RiaSingleton<RiaCameraManager>
    {
        #region MEMBER
        private RiaFlexibleCamera flexCamera_;
        private Transform cameraTrans_;
        private Camera[] cameras_;
        #endregion

        #region MAINFUNCTION
        /// <summary>
        /// 初期化
        /// </summary>
        public override void OnConstructor()
        {
            try
            {
                cameraTrans_ = GameObject.Find("Main Camera").transform;
            }
            catch (System.NullReferenceException e)
            {
                Debug.LogException(e);
                Debug.Log("/'Main Camera/'が存在しません");
            }
            cameras_ = cameraTrans_.GetComponentsInChildren<Camera>();
            flexCamera_ = new RiaFlexibleCamera(cameraTrans_, cameras_);
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnUpdate()
        {

        }
        #endregion
    }

}