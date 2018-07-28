using UnityEngine;

public abstract class Actor
{
    protected bool isActive { get; set; }
    protected GameObject go_ = null;
    protected Transform trans_ = null;

    public Actor()
    {
        go_ = null;
        trans_ = null;
        isActive = false;
    }

    public void Create(GameObject _go, Transform _trans)
    {
        this.go_ = _go;
        this.trans_ = _trans;

        OnCreate();
        isActive = true;
    }

    public void Run()
    {
        if (!isActive) { return; }

        OnRun();
    }

    public void Stop()
    {
        isActive = false;
    }

    protected abstract void OnCreate();
    protected abstract void OnRun();
}
