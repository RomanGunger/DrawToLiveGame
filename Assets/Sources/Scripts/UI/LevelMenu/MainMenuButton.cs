using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MainMenuButton : MenuButtonBase
{
    [SerializeField] Fader fader;

    protected override async void OnClickAction()
    {
        StartCoroutine(ShowAd());
    }

    IEnumerator ShowAd()
    {
        bool addWaiting = false;

        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        ulong diffInSeconds = ((ulong)DateTime.Now.Ticks - saveFile._lastTimeAdWatched) / TimeSpan.TicksPerSecond;
        float secondsLeft = (float)saveFile._timeWithoutAdsInSeconds - (float)diffInSeconds;

        Debug.Log($"{saveFile._timeWithoutAdsInSeconds} - {diffInSeconds} = {secondsLeft}");
        Debug.Log(saveFile._timeWithoutAdsInSeconds);
        Debug.Log(secondsLeft);

        if (Advertisement.isInitialized && secondsLeft <= 0)
        {
            addWaiting = true;
            InterstitialAd.UnityAdCompleted += () => {
                addWaiting = false;
                saveFile._lastTimeAdWatched = (ulong)DateTime.Now.Ticks;
                xmlManager.Save(saveFile);
            };
            InterstitialAd.instance.ShowAd();
        }

        while (addWaiting)
        {
            yield return null;
        }

        Time.timeScale = 1;

        TransitionToTheMainMenu();
    }

    private async void TransitionToTheMainMenu()
    {
        var scene = SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        await fader.FadeHandle(1f, 2f, false);
        scene.allowSceneActivation = true;
    }
}
