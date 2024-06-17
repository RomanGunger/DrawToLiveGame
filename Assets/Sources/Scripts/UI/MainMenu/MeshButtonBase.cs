using System;
using DG.Tweening;
using UnityEngine;

public abstract class MeshButtonBase : MonoBehaviour
{
    [SerializeField] protected MenuPanel panel;
    [SerializeField] protected AudioClip buttonSound;

    public static Action<bool> PanelOpened;

    Collider collider;

    protected virtual void Awake()
    {
        collider = GetComponent<Collider>();
        PanelOpened += OnPanelOpened;
        MenuPanel.OnPanelClosed += OnPanelOpened;
    }

    protected virtual void OnMouseUpAsButton()
    {
        OnClickAction();
        OnClickAnimation();
    }

    protected virtual async void OnClickAction()
    {
        await panel.gameObject.GetComponent<MenuPanel>().Open(1f);
        PanelOpened?.Invoke(true);
    }

    void OnPanelOpened(bool opened)
    {
        collider.enabled = !opened;
    }

    protected virtual async void OnClickAnimation()
    {
        collider.enabled = false;
        SoundFXManager.instance.PlaySoundFXClip(buttonSound, transform, 1f);
        await transform.DOPunchScale(new Vector3(-.1f, -.1f, -.1f), .2f, 0, 0).SetUpdate(true)
        .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
        collider.enabled = true;
    }

    private void OnDestroy()
    {
        PanelOpened -= OnPanelOpened;
        MenuPanel.OnPanelClosed -= OnPanelOpened;
    }
}
