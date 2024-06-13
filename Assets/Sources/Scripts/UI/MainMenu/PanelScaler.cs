using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelScaler : MonoBehaviour
{
    private void Awake()
    {
        float ratio = Screen.height / Screen.width;

        if(ratio > 1.95f)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float heigth = rectTransform.rect.height;

            RectTransformExtension.SetHeight(rectTransform, heigth * .85f);
        }
        else if (ratio < 1.1f)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float heigth = rectTransform.rect.height;

            RectTransformExtension.SetHeight(rectTransform, heigth * 1.1f);
        }
    }
}
