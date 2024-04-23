using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupEndGame : VisualElement
{
    public new class UxmlFactory : UxmlFactory<PopupEndGame> { }

    public PopupEndGame()
    {
        VisualElement newWindow = new VisualElement();
        hierarchy.Add(newWindow);
    }
}
