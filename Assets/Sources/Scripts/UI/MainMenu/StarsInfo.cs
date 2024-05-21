using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsInfo : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        LevelsPanel.ButtonsSet += SetStarsCount;
    }

    void SetStarsCount(int stars)
    {
        text.text = $"Stars: {stars}";
    }
}
