using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAnimationsHandler : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        var rnd = new System.Random();

        animator.SetFloat("CycleOffset", (float)rnd.NextDouble());
    }
}
