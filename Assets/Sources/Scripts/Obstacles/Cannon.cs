using System.Collections;
using System.Collections.Generic;
using CartoonFX;
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
        FinishLine.FinishLineReached += Begin;
        GameConditions.LevelStarted += Begin;

    }

    void Begin()
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
        FinishLine.FinishLineReached -= Begin;
        GameConditions.LevelStarted -= Begin;
    }
}
