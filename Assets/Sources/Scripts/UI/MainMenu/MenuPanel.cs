using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

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
        uiBlocker.gameObject.SetActive(true);
        gameObject.SetActive(true);
        await transform.DOMoveY(0, durration).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
    }

    public virtual async Task Close(float durration)
    {
        Debug.Log(gameObject.name + " Closing");
        await transform.DOMoveY(height, durration).AsyncWaitForCompletion();
        gameObject.SetActive(false);
        uiBlocker.gameObject.SetActive(false);
    }
}
