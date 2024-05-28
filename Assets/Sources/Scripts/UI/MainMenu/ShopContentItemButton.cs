using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopContentItemButton : MonoBehaviour
{
    protected Button button;

    [SerializeField] protected int price;

    protected XmlManager xmlManager;
    protected SaveFile saveFile;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickAnimation);
        button.onClick.AddListener(OnClickAction);
    }

    protected virtual async void OnClickAnimation()
    {
        await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0).SetUpdate(true)
.SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
    }

    protected virtual async void OnClickAction()
    {
        xmlManager = new XmlManager();
        saveFile = xmlManager.Load();
    }
}
