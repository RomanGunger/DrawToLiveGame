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
    [SerializeField] PauseMenuUIManager pauseMenuUIManager;

    [SerializeField] Fader fader;

    private async void Start()
    {
        UnitPosition.LevelFailed += Loose;
        UnitPosition.LevelPassed += Win;

        await FadeHandle(0, 2f, true);
        LevelStarted?.Invoke();
    }

    async Task FadeHandle(float value, float duration, bool hideFader)
    {
        fader.gameObject.SetActive(true);
        await fader.Fade(value, duration);

        if(hideFader)
            fader.gameObject.SetActive(false);
    }

    async void Loose()
    {
        await FadeHandle(0.7f, 2f, false);
        pauseMenuUIManager.OpenLooseUI();
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


        foreach(var unit in units)
        {
            unit.GetComponent<Animator>();
        }


        await FadeHandle(0.7f, 2f, false);
        pauseMenuUIManager.OpenWinUI();
        //winPanel.gameObject.SetActive(true);
        //winPanel.GetComponent<CanvasGroup>().DOFade(1, 2.5f);



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
