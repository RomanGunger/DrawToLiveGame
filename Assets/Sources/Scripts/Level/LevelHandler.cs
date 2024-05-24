using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] ScoreHandler scoreHandler;
    [SerializeField] StarsHandler starsHandler;

    XmlManager xmlManager;
    SaveFile saveFile;

    void Start()
    {
        FinishLine.FinishLineReached += Save;

        xmlManager = new XmlManager();
        saveFile = xmlManager.Load();
    }

    void Save()
    {
        SaveCurrency();
        HandleStars();

        xmlManager.Save(saveFile);
    }


    void SaveCurrency()
    {
        saveFile.currency += scoreHandler.CurrentScore;
    }


    void HandleStars()
    {
        if (saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Count - 1 >= LevelInfo.instance.CurentLevel
            && starsHandler.Stars > saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel])
        {
            Debug.Log(saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel]);
            saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel] = starsHandler.Stars;
        }
        else
        {
            if (LevelInfo.instance.CurentLevel + 1 <= LevelInfo.instance.CurrentChapterLevelsCount)
            {
                saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Add(starsHandler.Stars);
            }
            else
            {
                if (LevelInfo.instance.ChaptersCount >= LevelInfo.instance.CurrentChapter + 1
                    && !saveFile._passedLevels.ContainsKey(LevelInfo.instance.CurrentChapter + 1))
                {
                    saveFile._passedLevels[LevelInfo.instance.CurrentChapter + 1].Add(0);
                }
            }
        }

        Debug.Log(saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Count);
        Debug.Log(LevelInfo.instance.CurentLevel);
        Debug.Log(starsHandler.Stars);
        Debug.Log(LevelInfo.instance.CurrentChapterLevelsCount);
    }
}