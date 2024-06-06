using UnityEngine;
using DG.Tweening;

public class PopupCloseButton : MenuButtonBase
{
    [SerializeField] GameObject popup;

    protected override async void OnClickAction()
    {
        await popup.transform.DOScale(new Vector3(0, 0, 0), .2f).SetUpdate(true).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        Destroy(popup);
    }

    protected override async void OnClickAnimation()
    {
        base.OnClickAnimation();

        await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0).SetUpdate(true)
        .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
    }
}
