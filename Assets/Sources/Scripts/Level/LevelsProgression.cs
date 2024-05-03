using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "LevelProgression/New Level Progression", order = 0)]
public class LevelsProgression : ScriptableObject
{
    [SerializeField] LevelProgressionClass[] levelProgression = null;

    public int GetUnitsCount(int level)
    {
        return levelProgression[level].unitsCount;
    }

    public string GetSceneName(int level)
    {
        return levelProgression[level].sceneName;
    }

    public int GetLevelsCount()
    {
        return levelProgression.Length;
    }


    [System.Serializable]
    class LevelProgressionClass
    {
        public int unitsCount;
        public string sceneName;
    }
}

