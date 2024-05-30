using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventsHandler : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] MenuPanel winPanel;
    [SerializeField] MenuPanel loosePanel;

    public static Action LevelStarted;

    private void Awake()
    {
        UnitPosition.LevelPassed += OnLevelPassed;
        UnitsSpawner.LevelFailed += OnLevelFailed;
    }

    async void OnLevelPassed()
    {
        Time.timeScale = 0;
        await winPanel.Open(1f);
        UnitPosition.LevelPassed -= OnLevelPassed;
        UnitsSpawner.LevelFailed -= OnLevelFailed;
    }

    async void OnLevelFailed()
    {
        await loosePanel.Open(1f);
        //Time.timeScale = 0;
        UnitPosition.LevelPassed -= OnLevelPassed;
        UnitsSpawner.LevelFailed -= OnLevelFailed;
    }

    private async void Start()
    {
        await fader.FadeHandle(0, 2f, true);
        LevelStarted?.Invoke();
    }

    private void OnDestroy()
    {


    }
}
