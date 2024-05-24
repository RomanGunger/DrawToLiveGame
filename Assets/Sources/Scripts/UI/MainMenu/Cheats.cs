using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
}
