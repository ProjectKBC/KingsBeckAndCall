using System.Collections;
using UnityEngine;

public class TestRiaBehaviorManager : RiaBehaviorManager<TestRiaBehavior>
{
    protected override void OnAwake()
    {
        //StartCoroutine(ShotBehabior(0.5f));
        ShotBehabiorAll();
    }

    protected override void OnUpdate()
    {

    }

    private IEnumerator ShotBehabior(float _span)
    {
        yield return new WaitForSeconds(_span);
        while (true)
        {
            for (int i = 0; i < behaviors.Length; ++i)
            {
                if (!behaviors[i].Alive)
                {
                    behaviors[i].WakeUp(Vector3.zero, Quaternion.identity);
                    break;
                }
            }

            yield return new WaitForSeconds(_span);
        }
    }

    private void ShotBehabiorAll()
    {
        for (int i = 0; i < behaviors.Length; ++i)
        {
            behaviors[i].WakeUp(Vector3.zero, Quaternion.identity);
        }
    }

}