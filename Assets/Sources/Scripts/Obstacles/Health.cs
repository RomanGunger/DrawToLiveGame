using UnityEngine;

public class Health : BaseObstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();
            var point = other.ClosestPoint(transform.position);

            unit.unitPosition.AddUnit(point + new Vector3(0,1,0));
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }
}
