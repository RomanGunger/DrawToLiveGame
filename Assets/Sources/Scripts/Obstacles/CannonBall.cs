using UnityEngine;

public class CannonBall : BaseObstacle
{
    [SerializeField] float speed = 10f;
    [SerializeField] Renderer ballRenderer;
    [SerializeField] GameObject speedEffect;
    float duration = 1.3f;

    bool action = true;

    float startTime;

    void Start()
    {
        startTime = Time.time;
        Destroy(gameObject, duration + 2f);
    }

    void Update()
    {
        if (action)
        {
            if (Time.time < startTime + duration)
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            else
            {
                action = false;

                DeactivateCannonBall();
            }
        }
    }

    private void DeactivateCannonBall()
    {
        GetComponent<Collider>().enabled = false;
        ballRenderer.enabled = false;
        Destroy(speedEffect);
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
