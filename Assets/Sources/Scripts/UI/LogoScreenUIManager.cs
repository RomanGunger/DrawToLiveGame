using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScreenUIManager : MonoBehaviour
{
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
