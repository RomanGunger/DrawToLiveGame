using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] Fader fader;

    UIDocument uiDocument;
    VisualElement rootElement;

    Button playButton;

    [SerializeField] LevelsProgression levelsProgression;

    private async void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        playButton = rootElement.Q<Button>("play-button");
        playButton.RegisterCallback<ClickEvent>(PlayButton);

        await FadeHandle(0f, 2f, false);
    }

    async void PlayButton(ClickEvent evt)
    {
        var xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        await FadeHandle(1f, 2f, true);
        SceneManager.LoadSceneAsync(levelsProgression.GetSceneName(saveFile._level), LoadSceneMode.Single);
    }

    async Task FadeHandle(float value, float duration, bool hideFader)
    {
        Time.timeScale = 1;

        if (fader == null)
            return;

        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);

        fader.gameObject.SetActive(hideFader);

    }
}
