using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MenuButtonBase
{
    [SerializeField] Fader fader;

    protected override async void OnClickAction()
    {
        Time.timeScale = 1;

        var scene = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        await fader.FadeHandle(1f, 2f, false);
        scene.allowSceneActivation = true;
    }
}
