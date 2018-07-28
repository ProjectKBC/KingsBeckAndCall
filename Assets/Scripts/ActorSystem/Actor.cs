using UnityEngine;

public abstract class Actor
{
    private static readonly Vector3 VECTOR_ONE = Vector3.one;
    private static readonly Quaternion ROTATE_NONE = Quaternion.identity;

    protected bool isActive { get; set; }
    protected GameObject go_ = null;
    protected Transform trans_ = null;

    public Actor()
    {
        go_ = null;
        trans_ = null;
        isActive = false;
    }

    public Actor(GameObject _gameObject, Vector3 _localPosition)
    {
        this.go_ = _gameObject;
        this.trans_ = go_.transform;
        this.trans_.localPosition = _localPosition;
    }

    public Actor(string _name, Vector3 _localPosition)
    {
        this.go_ = new GameObject(_name);
        this.trans_ = go_.transform;
        this.trans_.localRotation = ROTATE_NONE;
        this.trans_.localScale = VECTOR_ONE;
        this.trans_.localPosition = _localPosition;
    }

    public void Create()
    {
        OnCreate();
        isActive = true;
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
