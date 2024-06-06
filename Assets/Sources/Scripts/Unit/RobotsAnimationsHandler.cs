public class RobotsAnimationsHandler : UnitAnimationsHandler
{
    Moving moving;

    protected override void Start()
    {
        base.Start();

        moving = GetComponentInParent<Moving>();

        var rnd = new System.Random();

        animator.SetInteger("RandomIdle", rnd.Next(0, animator.GetInteger("AnimationsCount")));
        animator.SetInteger("RandomRun", rnd.Next(0, animator.GetInteger("AnimationsCount")));
    }

    void Update()
    {
        animator.SetFloat("Speed", moving.Speed);
    }
}
