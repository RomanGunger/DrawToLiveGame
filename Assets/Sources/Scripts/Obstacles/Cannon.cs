using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject dieEffect;
    [SerializeField] Transform firePosition;
    [SerializeField] float fireRate = 2f;

    private float nextFire = 0.0f;

    bool isSleeping = true;

    private void Start()
    {
        FinishLine.FinishLineReached += BeginStop;
        PauseMenuUIManager.LevelStarted += BeginStop;

    }

    void BeginStop()
    {
        isSleeping = isSleeping ? false : true;
    }

    void Update()
    {
        if (!isSleeping)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject clone = Instantiate(projectile, firePosition.position, firePosition.rotation);
                clone.GetComponent<BaseObstacle>().dieEffect = dieEffect;
                GameObject fire = Instantiate(fireEffect, firePosition.position, firePosition.rotation);
                clone.GetComponent<BaseObstacle>();
            }
        }
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= BeginStop;
        PauseMenuUIManager.LevelStarted -= BeginStop;
    }
}
