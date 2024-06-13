using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public abstract class LevelButtonBase : MenuButtonBase
{
    protected override async void OnClickAction()
    {
        StartCoroutine(ShowAd(ActionAfterAd));
    }

    IEnumerator ShowAd(Action actionAfterAd)
    {
        bool addWaiting = false;

        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        ulong diffInSeconds = ((ulong)DateTime.Now.Ticks - saveFile._lastTimeAdWatched) / TimeSpan.TicksPerSecond;
        float secondsLeft = (float)saveFile._timeWithoutAdsInSeconds - (float)diffInSeconds;

        Debug.Log($"{saveFile._timeWithoutAdsInSeconds} - {diffInSeconds} = {secondsLeft}");
        Debug.Log(saveFile._timeWithoutAdsInSeconds);
        Debug.Log(secondsLeft);

        if (Advertisement.isInitialized && secondsLeft <= 0 && InterstitialAd.instance.AdLoaded)
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

        InterstitialAd.UnityAdCompleted -= () => {
            addWaiting = false;
            saveFile._lastTimeAdWatched = (ulong)DateTime.Now.Ticks;
            xmlManager.Save(saveFile);
        };

        actionAfterAd();
    }

    public abstract void ActionAfterAd();
}
