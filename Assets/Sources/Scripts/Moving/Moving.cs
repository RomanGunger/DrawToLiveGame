using Dreamteck.Splines;
using UnityEngine;

public class Moving : MonoBehaviour
{
    SplineFollower splineFollower;


    private void Start()
    {
        FinishLine.FinishLineReached += Stop;
        GameConditions.LevelStarted += Begin;
        UnitPosition.LevelFailed += Stop;

        splineFollower = GetComponent<SplineFollower>();
    }

    void Begin()
    {
        splineFollower.followSpeed = 3.5f;

        foreach (var animator in GetComponentsInChildren<Animator>())
        {
            animator.SetFloat("Speed", splineFollower.followSpeed);
        }
    }

    void Stop()
    {
        splineFollower.followSpeed = 0;
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= Stop;
        GameConditions.LevelStarted -= Begin;
    }
}
