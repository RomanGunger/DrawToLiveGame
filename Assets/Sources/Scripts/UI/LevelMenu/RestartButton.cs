using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : LevelButtonBase
{
    [SerializeField] Fader fader;
    [SerializeField] PopupsDatabase popupsDatabase;
    [SerializeField] Canvas popupParrentCanvas;

    public override async void OnClickAction()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        if (saveFile._lives <= 0)
        {
            Debug.Log("popup");
            GameObject outOfLivesPopup = popupsDatabase.GetPopup("outOfLivesPopup");
            if (outOfLivesPopup.TryGetComponent<BasePopupWindow>(out BasePopupWindow popup))
            {
                popup.InstantiatePopup(popupParrentCanvas.transform);
                Debug.Log("popup");
            }
            return;
        }

        if (LevelInfo.instance.CurrentChapterLevelsCount >= LevelInfo.instance.CurentLevel + 1 && saveFile._lives > 0)
        {
            Time.timeScale = 1;

            var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            scene.allowSceneActivation = false;
            await fader.FadeHandle(1f, 2f, false);
            scene.allowSceneActivation = true;
        }

    }
}
