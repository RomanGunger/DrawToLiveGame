using UnityEngine;

[CreateAssetMenu(fileName = "ChapterProgression", menuName = "ChapterProgression/New Chapter Progression", order = 3)]
public class ChapterProgression : ScriptableObject
{
    [SerializeField] ChapterProgressionClass[] chapterProgression = null;


    public int GetchaptersCount()
    {
        return chapterProgression.Length;
    }


    public string GetChaptersName(int chapter)
    {
        return chapterProgression[chapter].chaptersName;
    }

    public LevelsProgression GetLevelsProgression(int chapter)
    {
        return chapterProgression[chapter].levelsProgression;
    }

    [System.Serializable]
    class ChapterProgressionClass
    {
        public LevelsProgression levelsProgression;
        public string chaptersName;
    }
}

