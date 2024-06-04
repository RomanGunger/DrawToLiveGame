using UnityEngine;

public class FootStepsManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PauseButton.PausePressed += Stop;
        MenuPause.PauseEnd += Play;
        FinishLine.FinishLineReached += Stop;
        UnitsSpawner.LevelFailed += Stop;
        LevelEventsHandler.LevelStarted += Play;
    }

    private void OnDestroy()
    {
        PauseButton.PausePressed -= Stop;
        MenuPause.PauseEnd -= Play;
        FinishLine.FinishLineReached -= Stop;
        UnitsSpawner.LevelFailed -= Stop;
        LevelEventsHandler.LevelStarted -= Play;
    }

    void Stop()
    {
        audioSource.Stop();
    }

    void Play()
    {
        audioSource.Play();
    }

}
