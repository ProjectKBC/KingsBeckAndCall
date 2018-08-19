using UnityEngine;

public class TestRiaBehavior : RiaBehavior
{
    private Vector3 directionOfTravel = Vector3.zero;
    protected override void OnInit()
    {

    }

    protected override void OnRun()
    {
        MoveToRandomOrientation();
        //MoveDown();

        if (elapsedTime >= 5.0)
        {
            //this.Sleep();
        }
    }

    private void MoveToRandomOrientation()
    {
        trans_.position += directionOfTravel;
    }

    private void MoveDown()
    {
        trans_.position += Vector3.down * 3;
    }

    protected override void OnSleep()
    {

    }

    protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
    {
        float rad = Random.Range(0.0f, 2.0f * Mathf.PI);
        directionOfTravel = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}