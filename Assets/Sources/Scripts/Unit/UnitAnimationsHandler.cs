using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationsHandler : MonoBehaviour
{
    Animator animator;
    Moving moving;

    private void Start()
    {
        animator = GetComponent<Animator>();
        moving = GetComponentInParent<Moving>();

        var rnd = new System.Random();

        animator.SetInteger("RandomIdle", rnd.Next(0, animator.GetInteger("AnimationsCount")));
        animator.SetInteger("RandomRun", rnd.Next(0, animator.GetInteger("AnimationsCount")));
        animator.SetFloat("CycleOffset", (float)rnd.NextDouble());
    }

    private void Update()
    {
        animator.SetFloat("Speed", moving.Speed);
    }
}
