using UnityEngine;

public class TestMonoBehabior : MonoBehaviour
{
    private Vector3 directionOfTravel = Vector3.zero;
    private Transform trans_ = null;

    private void Awake()
    {
        trans_ = this.transform;
        float rad = Random.Range(0.0f, 2.0f * Mathf.PI);
        directionOfTravel = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    private void Update()
    {
        MoveToRandomOrientation();
        //MoveDown();
    }

    private void MoveToRandomOrientation()
    {
        trans_.position += directionOfTravel;
    }

    private void MoveDown()
    {
        trans_.position += Vector3.down * 3;
    }
}
