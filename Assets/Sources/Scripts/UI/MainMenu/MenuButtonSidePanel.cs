using DG.Tweening;
using UnityEngine;

public class MenuButtonSidePanel : MenuButtonBase
{
    [SerializeField] protected MenuPanel panel;

    protected override async void OnClickAction()
    {
        await panel.gameObject.GetComponent<MenuPanel>().Open(1f);
    }

    protected override async void OnClickAnimation()
    {
        base.OnClickAnimation();

        await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0).SetUpdate(true)
        .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
    }
}
