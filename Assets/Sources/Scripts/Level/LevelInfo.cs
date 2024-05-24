using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;

    public int UnitsCount { get; set; }
    public int CurentLevel { get; set; }
    public int CurrentChapter { get; set; }
    public int CurrentChapterLevelsCount { get; set; }
    public int ChaptersCount { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
