using DG.Tweening;
using UnityEngine;

public class MenuButtonSidePanel : MenuButtonBase
{
    [SerializeField] protected MenuPanel openPanel;

    protected override async void OnClickAction()
    {
        await openPanel.gameObject.GetComponent<MenuPanel>().Open(1f);
    }

    protected override async void OnClickAnimation()
    {
        await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0)
        .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
    }
}
