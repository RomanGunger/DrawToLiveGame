using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    public GameObject dieEffect;

    private void Start()
    {
        FinishLine.FinishLineReached += Stop;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();

            unit.unitPosition.RemoveUnit(other.gameObject.GetComponent<Unit>());

            if (!unit.unitPosition.units.Contains(other.gameObject.GetComponent<Unit>()))
            {
                Instantiate(dieEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();

            while (unit.unitPosition.units.Contains(other.gameObject.GetComponent<Unit>()))
            {
                unit.unitPosition.RemoveUnit(other.gameObject.GetComponent<Unit>());
                Instantiate(dieEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }

    protected virtual void Stop()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= Stop;
    }
}
