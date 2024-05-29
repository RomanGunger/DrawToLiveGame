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
        saveFile._currency += scoreHandler.CurrentScore;
    }


    void HandleStars()
    {
        if(LevelInfo.instance.CurentLevel + 1 == LevelInfo.instance.CurrentChapterLevelsCount
            && LevelInfo.instance.ChaptersCount >= LevelInfo.instance.CurrentChapter + 1
            && !saveFile._passedLevels.ContainsKey(LevelInfo.instance.CurrentChapter + 1))
        {
            saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Add(starsHandler.Stars);
            saveFile._passedLevels.Add(LevelInfo.instance.CurrentChapter + 1, new List<int> { });
            return;
        }

        if (saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Count - 1 >= LevelInfo.instance.CurentLevel
            && starsHandler.Stars > saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel])
        {
            saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel] = starsHandler.Stars;
        }
        else
        {
            if (LevelInfo.instance.CurentLevel + 1 <= LevelInfo.instance.CurrentChapterLevelsCount)
            {
                saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Add(starsHandler.Stars);
            }
        }
    }
}
