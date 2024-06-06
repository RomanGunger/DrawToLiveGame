using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        ScoreHandler.ScoreAdded += OnScoreAdded;
    }

    public void OnScoreAdded(int value)
    {
        text.text = value.ToString();
    }
}
