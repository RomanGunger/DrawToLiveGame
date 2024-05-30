using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupCloseButton : MonoBehaviour
{
    [SerializeField] GameObject popup;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    public async void Close()
    {
        await popup.transform.DOScale(new Vector3(0, 0, 0), .2f).SetUpdate(true).SetEase(Ease.InOutQuad).AsyncWaitForCompletion();
        Destroy(popup);
    }
}
