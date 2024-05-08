using UnityEngine;

public class Mine : BaseObstacle
{
    [SerializeField][Range(0, 4f)] float explosionRadius = 2f;
    [SerializeField] GameObject mineExplosionEffect;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(mineExplosionEffect, transform.position, Quaternion.identity);

            foreach (Collider inExp in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                if (inExp.gameObject.tag == "Player")
                {
                    Debug.Log("playre");

                    var unit = inExp.gameObject.GetComponent<Unit>();
                    UnitKilled?.Invoke(unit);

                    Instantiate(dieEffect,
                        unit.transform.position + new Vector3(0, unit.GetComponent<CapsuleCollider>().height, 0)
                        , Quaternion.identity);
                    Destroy(unit.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
