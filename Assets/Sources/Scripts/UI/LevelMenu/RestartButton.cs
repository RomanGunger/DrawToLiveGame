using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MenuButtonBase
{
    [SerializeField] Fader fader;
    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas; 

    protected override async void OnClickAction()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        if (saveFile._lives <= 0)
        {
            GameObject outOfLivesPopup = popupsDatabase.GetPopup("outOfLivesPopup");
            if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
            {
                popup.InstantiatePopup(popupParrentCanvas.transform);
            }
            return;
        }

        if (LevelInfo.instance.CurrentChapterLevelsCount >= LevelInfo.instance.CurentLevel + 1 && saveFile._lives > 0)
        {
            Time.timeScale = 1;
            saveFile._lives --;
            xmlManager.Save(saveFile);

            var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            scene.allowSceneActivation = false;
            await fader.FadeHandle(1f, 2f, false);
            scene.allowSceneActivation = true;
        }

    }
}
