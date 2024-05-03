using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore { get; private set; } = 0;

    public static Action<int> ScoreAdded;

    private void Start()
    {
        CurrencyItem.ScoreItemCollected += OnScoreItemCollected;
    }

    void OnScoreItemCollected()
    {
        CurrentScore++;

        ScoreAdded?.Invoke(CurrentScore);
    }
}
