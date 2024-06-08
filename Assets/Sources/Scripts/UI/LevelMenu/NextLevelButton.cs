using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : LevelButtonBase
{
    [SerializeField] Fader fader;
    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas;

    public override async void ActionAfterAd()
    {
        Time.timeScale = 1;

        if (LevelInfo.instance.CurrentChapterLevelsCount >= LevelInfo.instance.CurentLevel + 1)
        {
            var scene = SceneManager.LoadSceneAsync(
                LevelInfo.instance.levelsProgression.GetSceneName(LevelInfo.instance.CurentLevel + 1), LoadSceneMode.Single);

            LevelInfo.instance.CurentLevel++;
            LevelInfo.instance.UnitsCount = LevelInfo.instance.levelsProgression.GetUnitsCount(LevelInfo.instance.CurentLevel + 1);

            XmlManager xmlManager = new XmlManager();
            SaveFile saveFile = xmlManager.Load();

            if (saveFile._lives > 0)
                saveFile._lives--;

            scene.allowSceneActivation = false;
            await fader.FadeHandle(1f, 2f, false);
            scene.allowSceneActivation = true;
        }
    }
}
