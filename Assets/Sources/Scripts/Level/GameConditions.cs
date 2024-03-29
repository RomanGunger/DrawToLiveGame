using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameConditions : MonoBehaviour
{
    public static Action LevelStarted;
    [SerializeField] LoosePanel loosePanel;
    [SerializeField] LoosePanel winPanel;

    [SerializeField] Fader fader;

    private async void Start()
    {
        UnitPosition.LevelFailed += Loose;
        UnitPosition.LevelPassed += Win;

        await fader.Fade(0, 2f);
        LevelStarted?.Invoke();
    }

    async void Loose()
    {
        await fader.Fade(0.9f, 2f);
        loosePanel.gameObject.SetActive(true);
        loosePanel.GetComponent<CanvasGroup>().DOFade(1, 2.5f);

        loosePanel.restartButton.onClick.AddListener(() => {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        });

        loosePanel.restartButton.enabled = true;
        loosePanel.restartButton.interactable = false;
        await loosePanel.restartButton.image.DOFade(1, 2f).AsyncWaitForCompletion();
        loosePanel.restartButton.interactable = true;
    }

    void Win(List<Unit> units)
    {
        foreach(var unit in units)
        {
            unit.GetComponent<Animator>();
        }
    }

    private void OnDestroy()
    {
        UnitPosition.LevelFailed -= Loose;
        UnitPosition.LevelPassed -= Win;
    }
}
