using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton化されたMonoBehavior
/// </summary>
/// <typeparam name="T">SingletonにしたいMonoBehaviorクラス</typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private bool isAwake = false;

    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError("<color=#f00>" + typeof(T) + "</color> is nothing");
                    Debug.Break();
                }

                if (!instance.isAwake)
                {
                    instance.SingltonAwake();
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// 非明示的なoverrideをしないで!!
    /// </summary>
    private void Awake()
    {
        this.SingltonAwake();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void SingltonAwake()
    {
        if (this == Instance)
        {
            if (!isAwake)
            {
                // 初期化処理

                OnSingltonAwake();
                isAwake = true;
            }

            return;
        }

        Debug.LogError("<color=#f00>" + typeof(T) + "</color> is duplicated");
        Debug.Break();
    }

    protected abstract void OnSingltonAwake();
}