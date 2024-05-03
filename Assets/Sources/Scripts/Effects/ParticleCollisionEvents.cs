using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParticleCollisionEvents : MonoBehaviour
{
    ParticleSystem particleSystem;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    [SerializeField] GameObject decal;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        for(int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = collisionEvents[i].intersection;

            var rnd = new System.Random();

            GameObject dust = Instantiate(decal, pos, Quaternion.identity, transform);
            float rndScale = (float)rnd.NextDouble() * dust.GetComponentInChildren<Transform>().localScale.x;
            float rndScaleClamped = Mathf.Clamp(rndScale, .2f, .5f);
            dust.GetComponentInChildren<Transform>().localScale = new Vector3(rndScaleClamped, rndScaleClamped, rndScaleClamped);

            Color color = dust.GetComponentInChildren<MeshRenderer>().material.color;
            color.a = 0;

            //dust.GetComponentInChildren<Material>().DOColor(color, 2f);
            dust.GetComponentInChildren<MeshRenderer>().material.DOColor(color, 3f);
            Destroy(dust.gameObject, 3f);
        }

    }
}
