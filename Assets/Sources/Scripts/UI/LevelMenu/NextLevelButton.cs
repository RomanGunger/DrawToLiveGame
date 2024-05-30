using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : LevelButtonBase
{
    [SerializeField] Fader fader;
    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas;

    public override async void OnClickAction()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        Time.timeScale = 1;
     
        if(LevelInfo.instance.CurrentChapterLevelsCount >= LevelInfo.instance.CurentLevel + 1 && saveFile._lives > 0)
        {
            if(saveFile._lives <= 0)
            {
                GameObject outOfLivesPopup = popupsDatabase.GetPopup("outOfLivesPopup");
                if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
                {
                    popup.InstantiatePopup(popupParrentCanvas.transform);
                }
                return;
            }

            var scene = SceneManager.LoadSceneAsync(
                LevelInfo.instance.levelsProgression.GetSceneName(LevelInfo.instance.CurentLevel + 1), LoadSceneMode.Single);

            LevelInfo.instance.CurentLevel++;
            LevelInfo.instance.UnitsCount = LevelInfo.instance.levelsProgression.GetUnitsCount(LevelInfo.instance.CurentLevel + 1);

            scene.allowSceneActivation = false;
            await fader.FadeHandle(1f, 2f, false);
            scene.allowSceneActivation = true;
        }
    }
}
