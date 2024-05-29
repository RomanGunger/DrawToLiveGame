using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikesSet : BaseObstacle
{
    [SerializeField] Transform spikes;
    BoxCollider collider;

    [SerializeField] float fireRate = 5f;

    private float nextFire = 0.0f;

    bool isUp = false;

    private void Start()
    {
        System.Random rnd = new System.Random();

        collider = GetComponent<BoxCollider>();
        nextFire = rnd.Next(0, (int)fireRate);
    }

    private void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SetSpikes();
        }
    }

    void SetSpikes()
    {
        spikes.DOLocalMoveY(isUp ? -collider.size.y : 0f, .1f);
        Debug.Log(collider.size.y);
        isUp = !isUp;
        collider.enabled = isUp;
    }
}
