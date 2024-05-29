using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCurrencyBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        UpdateCurrency(saveFile._currency);
    }

    public void UpdateCurrency(int currency)
    {
        text.text = currency.ToString();
    }
}
