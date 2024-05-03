using Dreamteck.Splines;
using UnityEngine;

public class Moving : MonoBehaviour
{
    SplineFollower splineFollower;
    [SerializeField] float followSpeed = 3.5f;
    public float Speed
    {
        get
        {
            return splineFollower.followSpeed;
        }
        private set { }
    }

    private void Start()
    {
        FinishLine.FinishLineReached += Stop;
        PauseMenuUIManager.LevelStarted += Begin;
        UnitsSpawner.LevelFailed += Stop;

        splineFollower = GetComponent<SplineFollower>();
        splineFollower.followSpeed = 0;
    }

    void Begin()
    {
        splineFollower.followSpeed = followSpeed;
    }

    void Stop()
    {
        splineFollower.followSpeed = 0;
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= Stop;
        PauseMenuUIManager.LevelStarted -= Begin;
    }
}
