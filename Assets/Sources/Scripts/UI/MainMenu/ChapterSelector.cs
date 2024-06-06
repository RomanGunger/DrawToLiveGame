using UnityEngine;
using System;

public class ChapterSelector : MonoBehaviour
{
    [SerializeField] ChapterProgression chapterProgression;
    [SerializeField] LevelsPanel levelsPanel;

    public static Action<string> ChapterNameChanged;

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
        ChapterNameChanged?.Invoke(chapterProgression.GetChaptersName(currentChapter));

        if (saveFile._passedLevels.ContainsKey(currentChapter))
        {
            levelsPanel.SetButtons(chapterProgression.GetLevelsProgression(currentChapter)
            , saveFile._passedLevels[currentChapter], currentChapter);
        }
        else //If we didn't passed the chapter we set the passed levels value to "null"
        {
            levelsPanel.SetButtons(chapterProgression.GetLevelsProgression(currentChapter)
            , null, currentChapter);
        }

        LevelInfo.instance.CurrentChapterLevelsCount = chapterProgression.GetLevelsProgression(currentChapter).GetLevelsCount();
        LevelInfo.instance.ChaptersCount = chaptersCount;

        LevelInfo.instance.chapterProgression = chapterProgression;
        LevelInfo.instance.levelsProgression = chapterProgression.GetLevelsProgression(currentChapter);

        saveFile._currentChapter = currentChapter;
        xmlManager.Save(saveFile);
    }
}
