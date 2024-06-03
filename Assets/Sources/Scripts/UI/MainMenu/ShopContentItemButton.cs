using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopContentItemButton : MonoBehaviour
{
    protected Button button;

    [SerializeField] protected int price;
    [SerializeField] protected AudioClip buttonSound;

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
        button.interactable = false;
        SoundFXManager.instance.PlaySoundFXClip(buttonSound, transform, 1f);
        await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0).SetUpdate(true)
.SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
        button.interactable = true;
    }

    protected virtual async void OnClickAction()
    {
        xmlManager = new XmlManager();
        saveFile = xmlManager.Load();
    }
}
