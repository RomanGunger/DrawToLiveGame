using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(UIDocument))]
public class PauseMenuUIManager : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement rootElement;
    VisualElement pauseElement;
    VisualElement bodyItemsPauseElement;

    VisualElement winElement;
    VisualElement bodyItemsWinElement;

    VisualElement looseElement;
    VisualElement bodyItemsLooseElement;

    VisualElement overlay;
    Button pauseButton;
    Button continueButton;
    List<Button> restartButons;
    List<Button> menuButtons;
    Button nextLevelButton;

    [SerializeField] LevelsProgression levelsProgression;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        pauseButton = rootElement.Q<Button>("pause-button");
        continueButton = rootElement.Q<Button>("continue-button");
        restartButons = rootElement.Query<Button>("restart-button").ToList();
        nextLevelButton = rootElement.Q<Button>("next-level-button");
        menuButtons = rootElement.Query<Button>("menu-button").ToList();

        overlay = rootElement.Q(className: "overlay");
        pauseElement = rootElement.Q(className: "pause");
        bodyItemsPauseElement = rootElement.Q(className: "body-items-pause");

        winElement = rootElement.Q(className: "win-panel");
        bodyItemsWinElement = rootElement.Q(className: "body-items-win");

        looseElement = rootElement.Q(className: "loose-panel");
        bodyItemsLooseElement = rootElement.Q(className: "body-items-loose");

        pauseElement.style.display = DisplayStyle.None;
        winElement.style.display = DisplayStyle.None;
        looseElement.style.display = DisplayStyle.None;

        pauseButton.RegisterCallback<ClickEvent>(PauseButon);
        continueButton.RegisterCallback<ClickEvent>(ContinueButton);

        foreach(var item in restartButons)
        {
            item.RegisterCallback<ClickEvent>(RestartButton);
        }

        nextLevelButton.RegisterCallback<ClickEvent>(NextLevelButton);

        foreach (var item in menuButtons)
        {
            item.RegisterCallback<ClickEvent>(MenuButton);
        }

        bodyItemsPauseElement.AddToClassList("transition-scale-0");
        bodyItemsWinElement.AddToClassList("transition-scale-0");
        bodyItemsLooseElement.AddToClassList("transition-scale-0");

        overlay.AddToClassList("transition-opacity-0");
    }

    void PauseButon(ClickEvent evt)
    {
        Time.timeScale = 0;

        pauseElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsPauseElement.RemoveFromClassList("transition-scale-0");
        bodyItemsPauseElement.AddToClassList("transition-scale-1");

        overlay.RemoveFromClassList("transition-opacity-0");
        overlay.AddToClassList("transition-opacity-1");
    }

    void RestartButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void ContinueButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        pauseElement.style.display = DisplayStyle.None;
        pauseButton.style.display = DisplayStyle.Flex;

        bodyItemsPauseElement.RemoveFromClassList("transition-scale-1");
        bodyItemsPauseElement.AddToClassList("transition-scale-0");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    void MenuButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
    }

    public void OpenWinUI()
    {
        Time.timeScale = 0;

        winElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsWinElement.RemoveFromClassList("transition-scale-0");
        bodyItemsWinElement.AddToClassList("transition-scale-1");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    void NextLevelButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        SaveFile saveFile = XmlManager.Load();

        int nextLevel = saveFile._level + 1;

        nextLevel = saveFile._level + 1 >= levelsProgression.GetLevelsCount()
        ? 0 : saveFile._level + 1;

        saveFile._level = nextLevel;
        XmlManager.Save(saveFile);

        SceneManager.LoadSceneAsync(levelsProgression.GetSceneName(nextLevel), LoadSceneMode.Single);
    }

    public void OpenLooseUI()
    {
        Time.timeScale = 0;

        looseElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsLooseElement.RemoveFromClassList("transition-scale-0");
        bodyItemsLooseElement.AddToClassList("transition-scale-1");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    void Close()
    {

    }
}
