using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class GameConditions : MonoBehaviour
{
    public static Action LevelStarted;
    //[SerializeField] LoosePanel loosePanel;
    //[SerializeField] WinPanel winPanel;
    [SerializeField] LevelsProgression levelsProgression;

    [SerializeField] Fader fader;

    private async void Start()
    {
        UnitPosition.LevelFailed += Loose;
        UnitPosition.LevelPassed += Win;

        await FadeHandle(0, 2f);
        LevelStarted?.Invoke();
    }

    async Task FadeHandle(float value, float duration)
    {
        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);
        fader.gameObject.SetActive(false);
    }

    async void Loose()
    {
        await FadeHandle(0.9f, 2f);
        //loosePanel.gameObject.SetActive(true);
        //loosePanel.GetComponent<CanvasGroup>().DOFade(1, 2.5f);

        //loosePanel.restartButton.onClick.AddListener(() => {
        //    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        //});

        //loosePanel.restartButton.enabled = true;
        //loosePanel.restartButton.interactable = false;
        //await loosePanel.restartButton.image.DOFade(1, 2f).AsyncWaitForCompletion();
        //loosePanel.restartButton.interactable = true;
    }

    async void Win(List<Unit> units)
    {
        SaveFile saveFile = XmlManager.Load();

        foreach(var unit in units)
        {
            unit.GetComponent<Animator>();
        }


        await FadeHandle(0.9f, 2f);
        //winPanel.gameObject.SetActive(true);
        //winPanel.GetComponent<CanvasGroup>().DOFade(1, 2.5f);

        //winPanel.continueButton.onClick.AddListener(() => {
        //    int nextLevel = saveFile._level + 1;

        //    nextLevel = saveFile._level + 1 >= levelsProgression.GetLevelsCount()
        //    ? 0 : saveFile._level + 1;

        //    saveFile._level = nextLevel;
        //    XmlManager.Save(saveFile);

        //    SceneManager.LoadSceneAsync(levelsProgression.GetSceneName(nextLevel), LoadSceneMode.Single);
        //});

        //winPanel.continueButton.enabled = true;
        //winPanel.continueButton.interactable = false;
        //await winPanel.continueButton.image.DOFade(1, 2f).AsyncWaitForCompletion();
        //winPanel.continueButton.interactable = true;
    }

    private void OnDestroy()
    {
        UnitPosition.LevelFailed -= Loose;
        UnitPosition.LevelPassed -= Win;
    }
}
