using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] MenuLivesBox lives;
    [SerializeField] MenuCurrencyBox currency;

    public void AddLevel()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile._passedLevels.Clear();
        saveFile._passedLevels.Add(0, new List<int> { 0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 });
        xmlManager.Save(saveFile);

        Debug.Log(saveFile._passedLevels.Count);
    }

    public void DeleteSaves()
    {
        XmlManager xmlManager = new XmlManager();
        xmlManager.RemoveXML();
    }

    public void AddLives()
    {
        lives.PlusLive();
    }

    public void AddCurrency()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile._currency += 10000;

        currency.UpdateCurrency(saveFile._currency);
        xmlManager.Save(saveFile);
    }
}
