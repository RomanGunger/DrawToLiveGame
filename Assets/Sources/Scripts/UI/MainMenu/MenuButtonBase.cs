using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class MenuButtonBase : MonoBehaviour
{
    protected Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAnimation);
        button.onClick.AddListener(OnClickAction);
    }

    protected virtual void OnClickAnimation()
    {

    }

    protected abstract void OnClickAction();
}
