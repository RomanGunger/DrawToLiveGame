using UnityEngine;
using DG.Tweening;

public class Health : BaseObstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();
            var point = other.ClosestPoint(transform.position);

            UnitAdded?.Invoke(new Vector3(point.x, unit.transform.position.y, point.z));
            Destroy(gameObject);
        }
    }
}
