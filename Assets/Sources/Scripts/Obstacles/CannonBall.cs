using UnityEngine;

public class CannonBall : BaseObstacle
{
    [SerializeField] float speed = 10f;

    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var unit = other.gameObject.GetComponent<Unit>();

            UnitKilled?.Invoke(other.gameObject.GetComponent<Unit>());

            GameObject effect = Instantiate(dieEffect,
                other.transform.position + new Vector3(0, other.GetComponent<CapsuleCollider>().height, 0)
                , Quaternion.identity);
            var shape = effect.GetComponent<ParticleSystem>().shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            effect.transform.rotation = transform.rotation;


            Destroy(other.gameObject);
        }
    }
}
