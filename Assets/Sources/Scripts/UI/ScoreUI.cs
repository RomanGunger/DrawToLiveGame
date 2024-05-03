using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ScoreUI : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement rootElement;

    Label textElement;

    private void Start()
    {
        Score.ScoreAdded += OnScoreAdded;

        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        textElement = rootElement.Q<Label>("score-text");
    }

    public void OnScoreAdded(int value)
    {
        textElement.text = value.ToString();
    }
}
