using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore { get; private set; } = 0;

    public static Action<int> ScoreAdded;

    private void Start()
    {
        CurrencyItem.ScoreItemCollected += OnScoreItemCollected;
        FinishLine.FinishLineReached += SaveCurrency;
    }

    void OnScoreItemCollected()
    {
        CurrentScore++;

        ScoreAdded?.Invoke(CurrentScore);
    }

    void SaveCurrency()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile.currency += CurrentScore;
        xmlManager.Save(saveFile);
    }
}
