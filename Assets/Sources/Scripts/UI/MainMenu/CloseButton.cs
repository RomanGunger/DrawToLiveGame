using DG.Tweening;
using UnityEngine;

public class CloseButton : MenuButtonBase
{
    [SerializeField] Transform closeObject;

    protected override void OnClickAction()
    {
        closeObject.gameObject.SetActive(false);
    }
}
