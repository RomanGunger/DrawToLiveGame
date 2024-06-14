using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPanelScaler : MonoBehaviour
{
    private void Awake()
    {
        float ratio = Screen.height / Screen.width;

        if (ratio < 1.1f)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float heigth = rectTransform.rect.height;
            float width = rectTransform.rect.width;

            Debug.Log(heigth);
            Debug.Log(width);

            RectTransformExtension.SetHeight(rectTransform, heigth * .8f);
            RectTransformExtension.SetWidth(rectTransform, width * .8f);
        }
    }
}
