using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : LevelButtonBase
{
    [SerializeField] Fader fader;

    public override async void OnClickAction()
    {
        Time.timeScale = 1;
     
        if(LevelInfo.instance.CurrentChapterLevelsCount <= LevelInfo.instance.CurentLevel + 1)
        {
            var scene = SceneManager.LoadSceneAsync(LevelInfo.instance.CurentLevel + 1, LoadSceneMode.Single);
            scene.allowSceneActivation = false;
            await fader.FadeHandle(1f, 2f, false);
            scene.allowSceneActivation = true;
        }
    }
}
