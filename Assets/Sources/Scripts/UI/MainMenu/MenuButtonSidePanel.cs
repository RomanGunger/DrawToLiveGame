using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSidePanel : MenuButtonBase
{
    [SerializeField] protected MenuPanel openPanel;

    protected override void OnClickAction()
    {
        openPanel.gameObject.SetActive(true);
    }
}
