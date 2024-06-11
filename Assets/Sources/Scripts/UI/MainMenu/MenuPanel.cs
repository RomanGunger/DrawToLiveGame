using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    float height;

    [SerializeField] GameObject uiBlocker;

    private void Awake()
    {
        height = GetComponent<RectTransform>().sizeDelta.y;
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
        await transform.DOMoveY(0, durration).SetUpdate(true).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
    }

    public virtual async Task Close(float durration)
    {
        uiBlocker.GetComponent<Image>().DOFade(0f, durration);
        await transform.DOMoveY(height, durration).SetUpdate(true).AsyncWaitForCompletion();
        gameObject.SetActive(false);
        uiBlocker.gameObject.SetActive(false);
    }
}
