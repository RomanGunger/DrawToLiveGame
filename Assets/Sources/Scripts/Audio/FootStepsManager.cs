using UnityEngine;

public class FootStepsManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PauseButton.PausePressed += () => {
            audioSource.Stop();
        };
        MenuPause.PauseEnd += () => {
            audioSource.Play();
        };
        FinishLine.FinishLineReached += () => {
            audioSource.Stop();
        };
        UnitsSpawner.LevelFailed += () => {
            audioSource.Stop();
        };
        LevelEventsHandler.LevelStarted += () => {
            audioSource.Play();
        };
    }

}
