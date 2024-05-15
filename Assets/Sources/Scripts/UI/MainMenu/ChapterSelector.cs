using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterSelector : MonoBehaviour
{
    [SerializeField] ChapterProgression chapterProgression;
    [SerializeField] LevelsPanel levelsPanel;
    [SerializeField] TextMeshProUGUI chaptersName;
    int chaptersCount;
    int currentChapter;

    XmlManager xmlManager;
    SaveFile saveFile;

    private void Awake()
    {
        chaptersCount = chapterProgression.GetchaptersCount();
    }

    private void Start()
    {
        xmlManager = new XmlManager();
        saveFile = xmlManager.Load();

        currentChapter = saveFile._currentChapter;

        SetChapter();
    }

    public void NextChapter()
    {
        if (currentChapter + 1 > chaptersCount - 1)
            return;
        else
            currentChapter++;

        SetChapter();
    }

    public void PreviousChapter()
    {
        if (currentChapter - 1 < 0)
            return;
        else
            currentChapter--;

        SetChapter();
    }

    void SetChapter()
    {
        chaptersName.text = chapterProgression.GetChaptersName(currentChapter);

        int passedLevels = -1;
        if (saveFile._passedLevels.ContainsKey(currentChapter))
            passedLevels = saveFile._passedLevels[currentChapter];

        levelsPanel.SetButtons(chapterProgression.GetLevelsProgression(currentChapter)
            , passedLevels, currentChapter);

        saveFile._currentChapter = currentChapter;
        xmlManager.Save(saveFile);
    }
}
