using UnityEngine;
using DG.Tweening;

public class BasePopupWindow : MonoBehaviour
{
    [SerializeField] protected AudioClip popupSound;


    public void InstantiatePopup(Transform parrentCanvas)
    {
        GameObject popup = Instantiate(gameObject, parrentCanvas);
    }

    protected virtual void Start()
    {
        transform.localScale = new Vector3(0,0,0);
        transform.DOScale(new Vector3(1,1,1), .2f).SetUpdate(true).SetEase(Ease.InOutQuad);
        SoundFXManager.instance.PlaySoundFXClip(popupSound, transform, 1f);
    }
}
