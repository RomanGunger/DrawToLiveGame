using UnityEngine;

public class Saw : BaseObstacle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();

            UnitKilled?.Invoke(other.gameObject.GetComponent<Unit>());

            GameObject effect = Instantiate(dieEffect,
                other.transform.position + new Vector3(0, other.GetComponent<CapsuleCollider>().height, 0)
                , Quaternion.identity);
            var velocityOverLifetime = effect.GetComponent<ParticleSystem>().velocityOverLifetime;
            velocityOverLifetime.enabled = true;
            velocityOverLifetime.orbitalZ = 6;

            unit.DestroyUnit();
        }
    }
}
