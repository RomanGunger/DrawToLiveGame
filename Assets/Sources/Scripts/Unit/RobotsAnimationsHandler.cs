using UnityEngine;

public class RobotsAnimationsHandler : UnitAnimationsHandler
{
    protected override void Start()
    {
        base.Start();

        animator.SetInteger("RandomIdle", rnd.Next(0, animator.GetInteger("AnimationsCount")));
        animator.SetInteger("RandomRun", rnd.Next(0, animator.GetInteger("AnimationsCount")));
    }

    void Update()
    {
        animator.SetFloat("Speed", moving.Speed);
    }
}
