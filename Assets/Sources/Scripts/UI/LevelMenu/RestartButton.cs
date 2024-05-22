using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : LevelButtonBase
{
    [SerializeField] Fader fader;

    public override async void OnClickAction()
    {
        Time.timeScale = 1;

        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        await fader.FadeHandle(1f, 2f, false);
        scene.allowSceneActivation = true;
    }
}
