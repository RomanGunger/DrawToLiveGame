using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] MenuLivesBox lives;
    [SerializeField] GameObject buttonPrefab;

    GridLayoutGroup gridLayout;

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

        float width = resolution.x / 12;
        float ratio = resolution.y / resolution.x;
        if (ratio < 1.7f)
            ratio = 1.7f;
        float cellSize = ratio * width;
        gridLayout.cellSize = new Vector2(cellSize, cellSize * 1.15f);
    }

    public void SetButtons(LevelsProgression levelsProgression, int passedLevels, int chapter)
    {
        foreach (Transform trans in transform)
            Destroy(trans.gameObject);

        int levelsCount = levelsProgression.GetLevelsCount();

        for (int i = 0; i < levelsCount; i++)
        {
            string name = levelsProgression.GetSceneName(i);
            int unitsCount = levelsProgression.GetUnitsCount(i);
            int level = i;
            LevelButton button = Instantiate(buttonPrefab, transform).GetComponent<LevelButton>();
            button.SetText((i + 1).ToString());

            int lockLevels = 0;
            if (passedLevels != -1 && passedLevels < levelsProgression.GetLevelsCount())
                lockLevels++;

            if (i > lockLevels)
                button.LockButton();
            else button.UnLockButton();

            Button buttonComponent = button.GetButton();

            buttonComponent.onClick.AddListener(() =>
            {
                buttonComponent.transform.DOPunchScale(new Vector3(.9f, .9f, .9f), .5f)
                .SetEase(Ease.Linear);
            });

            buttonComponent.onClick.AddListener(() =>
                OnClickActionAsync(name, unitsCount, level, chapter));
        }
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
        Time.timeScale = 1;

        if (fader == null)
            return;

        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);

        fader.gameObject.SetActive(hideFader);
    }
}
