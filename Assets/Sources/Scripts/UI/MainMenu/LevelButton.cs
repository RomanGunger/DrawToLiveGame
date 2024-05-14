using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    Button button;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public Button GetButton()
    {
        return button;
    }
}
