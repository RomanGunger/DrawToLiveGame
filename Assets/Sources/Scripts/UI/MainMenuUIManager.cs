using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] UIDocument uiDoc;
    VisualElement rootElement;

    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;

        foreach (var item in rootElement.Query(className: "basePanel").ToList())
        {
            item.AddToClassList("bg-orange");
            var text = item.Q<Label>(className: "labelText");
            text.text = "1111111";

            item.RegisterCallback<MouseDownEvent>((itemClick) => {
                var item = itemClick.target as VisualElement;
                var text = item.Q<Label>(className: "labelText");
                Debug.Log($"Clicked: {text.text}");
            });
        }

        rootElement.Add(BuildPanel());
    }

    VisualElement BuildPanel()
    {
        var box = new VisualElement();
        box.AddToClassList("basePanel");
        box.AddToClassList("basePanelYellow");

        var label = new Label("newLabel");
        label.AddToClassList("text");

        box.Add(label);

        return box;
    }
}
