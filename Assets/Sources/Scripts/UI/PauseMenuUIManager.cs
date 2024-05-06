using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

[RequireComponent(typeof(UIDocument))]
public class PauseMenuUIManager : MonoBehaviour
{
    public static Action LevelStarted;

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
    [SerializeField] Fader fader;

    private async void Start()
    {
        UnitPosition.LevelPassed += OpenWinUI;
        UnitsSpawner.LevelFailed += OpenLooseUI;

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

        bodyItemsPauseElement.AddToClassList("transition-translate-down");
        bodyItemsWinElement.AddToClassList("transition-translate-down");
        bodyItemsLooseElement.AddToClassList("transition-translate-down");

        overlay.AddToClassList("transition-opacity-0");

        await FadeHandle(0, 2f, true);
        LevelStarted?.Invoke();
    }

    void PauseButon(ClickEvent evt)
    {
        Time.timeScale = 0;

        pauseElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsPauseElement.RemoveFromClassList("transition-translate-down");
        bodyItemsPauseElement.AddToClassList("transition-translate-up");

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

        bodyItemsPauseElement.RemoveFromClassList("transition-translate-up");
        bodyItemsPauseElement.AddToClassList("transition-translate-down");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    async void MenuButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        await FadeHandle(1f, 2f, false);
        SceneManager.LoadSceneAsync("Main_Menu", LoadSceneMode.Single);
    }

    async Task FadeHandle(float value, float duration, bool hideFader)
    {
        if (fader == null)
            return;

        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);

        if (hideFader)
            fader.gameObject.SetActive(false);
    }

    public async void OpenWinUI()
    {
        await FadeHandle(.7f, 2f, false);

        Time.timeScale = 0;

        winElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsWinElement.RemoveFromClassList("transition-translate-down");
        bodyItemsWinElement.AddToClassList("transition-translate-up");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    void NextLevelButton(ClickEvent evt)
    {
        Time.timeScale = 1;

        var xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        int nextLevel = saveFile._level + 1;

        nextLevel = saveFile._level + 1 >= levelsProgression.GetLevelsCount()
        ? 0 : saveFile._level + 1;

        saveFile._level = nextLevel;
        xmlManager.Save(saveFile);

        SceneManager.LoadSceneAsync(levelsProgression.GetSceneName(nextLevel), LoadSceneMode.Single);
    }

    public async void OpenLooseUI()
    {
        await FadeHandle(.7f, 2f, false);

        Time.timeScale = 0;

        looseElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsLooseElement.RemoveFromClassList("transition-translate-down");
        bodyItemsLooseElement.AddToClassList("transition-translate-up");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    private void OnDestroy()
    {
        UnitPosition.LevelPassed -= OpenWinUI;
        UnitsSpawner.LevelFailed -= OpenLooseUI;
    }
}
