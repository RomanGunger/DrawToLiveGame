using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    public GameObject dieEffect;

    public static Action<Unit> UnitKilled;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();

            UnitKilled?.Invoke(unit);

            Instantiate(dieEffect,
                unit.transform.position + new Vector3(0, unit.GetComponent<CapsuleCollider>().height, 0)
                , Quaternion.identity);
            Destroy(unit.gameObject);
        }
    }

    protected virtual void Stop()
    {
        GetComponent<Collider>().enabled = false;
    }
}
