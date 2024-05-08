using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class LogoScreenUIManager : MonoBehaviour
{
    //UIDocument uiDocument;
    //VisualElement rootElement;
    //VisualElement logo;

    //async void Start()
    //{
    //    uiDocument = GetComponent<UIDocument>();
    //    rootElement = uiDocument.rootVisualElement;

    //    logo = rootElement.Q(className: "logo");
    //    logo.AddToClassList("transition-opacity-0");

    //    await LoadScene("Loading_Screen");
    //}

    //async Task LoadScene(string sceneName)
    //{
    //    var asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    //    asyncSceneLoad.allowSceneActivation = false;

    //    //logo.style.transitionDuration = new List<TimeValue> { new(2, TimeUnit.Second) };
    //    logo.RemoveFromClassList("transition-opacity-0");
    //    logo.AddToClassList("transition-opacity-1");
    //    await Task.Delay(3000);
    //    asyncSceneLoad.allowSceneActivation = true;
    //}

    [SerializeField] Fader fader;

    private async void Start()
    {
        Color color = fader.GetComponent<Image>().color;
        color.a = 0;
        fader.GetComponent<Image>().color = color;

        var asyncSceneLoad = SceneManager.LoadSceneAsync("Loading_Screen", LoadSceneMode.Single);
        asyncSceneLoad.allowSceneActivation = false;

        await fader.Fade(1, 2f);
        await Task.Delay(1000);

        asyncSceneLoad.allowSceneActivation = true;
    }
}
