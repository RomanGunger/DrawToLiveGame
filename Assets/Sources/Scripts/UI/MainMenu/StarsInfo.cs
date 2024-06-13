using UnityEngine;
using TMPro;

public class StarsInfo : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelsPanel.StarsCountChanged += OnStarsCountChanged;
    }

    void OnStarsCountChanged(int stars)
    {
        text.text = $"Stars: {stars}";
    }
}
