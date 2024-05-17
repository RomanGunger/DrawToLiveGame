using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] MenuLivesBox lives;

    public void AddLevel()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        saveFile._passedLevels.Clear();
        saveFile._passedLevels.Add(0,-1);
        saveFile._passedLevels.Add(1,-1);
        xmlManager.Save(saveFile);
    }

    public void LoadLevels()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        foreach (var item in saveFile._passedLevels)
        {
            Debug.Log(item.Key + " : " + item.Value);
        }
    }

    public void DeleteSaves()
    {
        XmlManager xmlManager = new XmlManager();
        //SaveFile saveFile = xmlManager.Load();

        xmlManager.RemoveXML();
    }

    public void AddLives()
    {
        lives.PlusLive();
    }
}
