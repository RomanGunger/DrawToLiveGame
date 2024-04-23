using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

[RequireComponent(typeof(UIDocument))]
public class PauseMenuUIManager : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement rootElement;
    VisualElement pauseElement;
    VisualElement bodyItemsElement;
    VisualElement overlay;
    Button pauseButton;
    Button continueButton;
    Button restartButon;
    Button menuButton;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        pauseButton = rootElement.Q<Button>("pause-button");
        continueButton = rootElement.Q<Button>("continue-button");
        restartButon = rootElement.Q<Button>("restart-button");
        //menuButton = rootElement.Q<Button>("restart-button");

        overlay = rootElement.Q(className: "overlay");
        pauseElement = rootElement.Q(className: "pause");
        bodyItemsElement = rootElement.Q(className: "body-items");
        pauseElement.style.display = DisplayStyle.None;

        pauseButton.RegisterCallback<ClickEvent>(PauseButon);
        continueButton.RegisterCallback<ClickEvent>(ContinueButton);
        restartButon.RegisterCallback<ClickEvent>(RestartButton);

        bodyItemsElement.AddToClassList("transition-scale-0");
        overlay.AddToClassList("transition-opacity-0");
    }

    void PauseButon(ClickEvent evt)
    {
        pauseElement.style.display = DisplayStyle.Flex;
        pauseButton.style.display = DisplayStyle.None;

        bodyItemsElement.RemoveFromClassList("transition-scale-0");
        bodyItemsElement.AddToClassList("transition-scale-1");

        overlay.RemoveFromClassList("transition-opacity-0");
        overlay.AddToClassList("transition-opacity-1");
    }

    void RestartButton(ClickEvent evt)
    {

    }

    void ContinueButton(ClickEvent evt)
    {
        pauseElement.style.display = DisplayStyle.None;
        pauseButton.style.display = DisplayStyle.Flex;

        bodyItemsElement.RemoveFromClassList("transition-scale-1");
        bodyItemsElement.AddToClassList("transition-scale-0");

        overlay.RemoveFromClassList("transition-opacity-1");
        overlay.AddToClassList("transition-opacity-0");
    }

    void Close()
    {

    }
}
