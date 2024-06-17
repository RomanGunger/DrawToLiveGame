using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;

public class MenuPanel : MonoBehaviour
{
    float rectHeight;

    [SerializeField] GameObject uiBlocker;
    RectTransform rectTransform;

    Vector2 basePos;

    public static Action<bool> OnPanelClosed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Rect rect = rectTransform.rect;
        float rectHeight = rect.height;

        basePos = new Vector2(0, rectHeight);
        rectTransform.anchoredPosition = basePos;
    }

    public virtual async Task Open(float durration)
    {
        if (uiBlocker != null)
        {
            uiBlocker.gameObject.SetActive(true);
            uiBlocker.GetComponent<Image>().DOFade(.7f, durration).SetUpdate(true);
        }
        else
            Debug.LogError("NO UIBLOCKER ASSIGNED: MenuPanel");

        gameObject.SetActive(true);
        await rectTransform.DOAnchorPosY(rectHeight, durration).SetUpdate(true).SetEase(Ease.OutBounce).AsyncWaitForCompletion();

    }

    public virtual async Task Close(float durration)
    {
        uiBlocker.GetComponent<Image>().DOFade(0f, durration);
        await rectTransform.DOAnchorPos(basePos, durration).SetUpdate(true).AsyncWaitForCompletion();
        gameObject.SetActive(false);
        uiBlocker.gameObject.SetActive(false);
        OnPanelClosed?.Invoke(false);
    }
}
