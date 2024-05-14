using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] Fader fader;
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
        Debug.Log(width);
        float ratio = resolution.y / resolution.x;
        if (ratio < 1.7f)
            ratio = 1.7f;
        float cellSize = ratio * width;
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }

    public void SetButtons(LevelsProgression levelsProgression)
    {
        foreach (Transform trans in transform)
            Destroy(trans.gameObject);

        int levelsCount = levelsProgression.GetLevelsCount();

        for (int i = 0; i < levelsCount; i++)
        {
            string name = levelsProgression.GetSceneName(i);
            LevelButton button = Instantiate(buttonPrefab, transform).GetComponent<LevelButton>();
            button.SetText((i + 1).ToString());

            Button buttonComponent = button.GetButton();
            buttonComponent.onClick.AddListener(() =>
                OnClickActionAsync(name));
        }
    }

    async void OnClickActionAsync(string sceneName)
    {
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
