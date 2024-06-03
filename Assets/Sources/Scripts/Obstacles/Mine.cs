using UnityEngine;

public class Mine : BaseObstacle
{
    [SerializeField][Range(0, 4f)] float explosionRadius = 2f;
    [SerializeField] GameObject mineExplosionEffect;
    [SerializeField] AudioClip explosionSound;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(mineExplosionEffect, transform.position, Quaternion.identity);

            foreach (Collider inExp in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                if (inExp.gameObject.tag == "Player")
                {
                    var unit = inExp.gameObject.GetComponent<Unit>();
                    UnitKilled?.Invoke(unit);

                    Instantiate(dieEffect,
                        unit.transform.position + new Vector3(0, unit.GetComponent<CapsuleCollider>().height, 0)
                        , Quaternion.identity);
                    Destroy(unit.gameObject);
                }
            }

            if (explosionSound != null)
                SoundFXManager.instance.PlaySoundFXClip(explosionSound, transform, 1f);
            else
                Debug.LogError("No collectSound assigned: CorrencyItem");

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
