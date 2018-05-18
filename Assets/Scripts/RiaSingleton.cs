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
    /// シングルトン
    /// </summary>
    /// <typeparam name="T">継承されるクラス</typeparam>
    public abstract class RiaSingleton<T> where T : class, new()
    {
        private static readonly T _inst = new T();

        public static T GetInstance() { return _inst; }

        public abstract void OnConstructor();
        protected RiaSingleton()
        {
            OnConstructor();
            Debug.Assert(null == _inst);
        }
    }
}