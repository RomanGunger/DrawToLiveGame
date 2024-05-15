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

        text.text = saveFile.currency.ToString();
    }
}
