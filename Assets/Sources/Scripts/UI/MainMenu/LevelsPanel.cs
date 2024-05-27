using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] MenuLivesBox lives;
    [SerializeField] GameObject buttonPrefab;

    public static Action<int> ButtonsSet;

    GridLayoutGroup gridLayout;
    int starsCount;

    private void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();

        SetButtonsSize();
    }

    private void SetButtonsSize()
    {
        Vector2 resolution = Vector2.zero;
        Resolution[] resolutions = Screen.resolutions;

        foreach (var res in resolutions)
        {
            resolution.x = res.width;
            resolution.y = res.height;
        }

        float width = resolution.x / 15;
        float ratio = resolution.y / resolution.x;
        if (ratio < 1.7f)
            ratio = 1.7f;
        float cellSize = ratio * width;
        gridLayout.cellSize = new Vector2(cellSize, cellSize * 1.15f);
    }

    public void SetButtons(LevelsProgression levelsProgression, List<int> passedLevels, int chapter)
    {
        starsCount = 0;

        foreach (Transform trans in transform)
            Destroy(trans.gameObject);

        int levelsCount = levelsProgression.GetLevelsCount();
        int passedLevelCount = 0;

        if (passedLevels != null)
            passedLevelCount = passedLevels.Count;

        for (int i = 0; i < levelsCount; i++)
        {
            string name = levelsProgression.GetSceneName(i);
            int unitsCount = levelsProgression.GetUnitsCount(i);
            int level = i;
            LevelButton button = Instantiate(buttonPrefab, transform).GetComponent<LevelButton>();
            button.SetText((i + 1).ToString());

            if (passedLevels == null)
            {
                button.LockButton();
                continue;
            }

            if (i < passedLevelCount)
            {
                int levelStars = passedLevels[i];
                starsCount += levelStars;
                button.UnLockButton(levelStars);
            }
            else if(i == passedLevelCount)
            {
                button.UnLockButton();
            }
            else
                button.LockButton();

            Button buttonComponent = button.GetButton();

            buttonComponent.onClick.AddListener(async () =>
            {
                buttonComponent.interactable = false;
                await buttonComponent.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0)
                .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
                buttonComponent.interactable = true;
            });

            buttonComponent.onClick.AddListener(() =>
                OnClickActionAsync(name, unitsCount, level, chapter));
        }

        ButtonsSet?.Invoke(starsCount);
    }

    async void OnClickActionAsync(string sceneName, int unitsCount, int level, int chapter)
    {
        int currentChapter = chapter;

        if (lives.Lives <= 0)
            return;

        lives.MinusLive();

        LevelInfo.instance.UnitsCount = unitsCount;
        LevelInfo.instance.CurentLevel = level;
        LevelInfo.instance.CurrentChapter = currentChapter;

        var asyncSceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncSceneLoad.allowSceneActivation = false;
        await FadeHandle(1, 2f, true);
        asyncSceneLoad.allowSceneActivation = true;
    }

    async Task FadeHandle(float value, float duration, bool hideFader)
    {
        if (fader == null)
            return;

        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);

        fader.gameObject.SetActive(hideFader);
    }
}
