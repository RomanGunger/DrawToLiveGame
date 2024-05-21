using DG.Tweening;
using UnityEngine;

public class CloseButton : MenuButtonBase
{
    [SerializeField] Transform closeObject;

    protected override void OnClickAction()
    {
        if (closeObject.TryGetComponent<MenuPanel>(out MenuPanel menuPanel))
        {
            menuPanel.Close(.3f);
        }
    }
}
