using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenuUIManager : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement rootElement;

    Button playButton;

    [SerializeField] LevelsProgression levelsProgression;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        playButton = rootElement.Q<Button>("play-button");
        playButton.RegisterCallback<ClickEvent>(PlayButton);
    }

    void PlayButton(ClickEvent evt)
    {
        var xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        SceneManager.LoadSceneAsync(levelsProgression.GetSceneName(saveFile._level), LoadSceneMode.Single);
    }
}
