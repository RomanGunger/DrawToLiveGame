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
        saveFile._passedLevels.Add(0, new List<int> { 0 });
        xmlManager.Save(saveFile);
        //xmlManager.CloudSave(saveFile);
    }

    public void LoadLevels()
    {
        XmlManager xmlManager = new XmlManager();
        SaveFile saveFile = xmlManager.Load();

        //xmlManager.CloudLoad();
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
