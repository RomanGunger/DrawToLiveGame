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
}
