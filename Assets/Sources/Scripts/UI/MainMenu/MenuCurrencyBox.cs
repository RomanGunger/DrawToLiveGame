using UnityEngine;
using TMPro;

public class MenuCurrencyBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        RewardedAdsButton.CurrencyAdded += OnCurrencyUpdated;
        ShopContentItemButtonFullLives.CurrencyAdded += OnCurrencyUpdated;
    }

    void Start()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        OnCurrencyUpdated(saveFile._currency);
    }

    public void OnCurrencyUpdated(int currency)
    {
        text.text = currency.ToString();
    }
}
