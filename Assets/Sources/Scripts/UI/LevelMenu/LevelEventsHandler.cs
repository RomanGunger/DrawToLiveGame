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
        UnitPosition.LevelPassed -= OnLevelPassed;
        UnitsSpawner.LevelFailed -= OnLevelFailed;
        Time.timeScale = 0;
        await winPanel.Open(1f);
    }

    async void OnLevelFailed()
    {
        UnitPosition.LevelPassed -= OnLevelPassed;
        UnitsSpawner.LevelFailed -= OnLevelFailed;
        await loosePanel.Open(1f);
        //Time.timeScale = 0;
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
