using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "LevelProgression/New Level Progression", order = 0)]
public class LevelsProgression : ScriptableObject
{
    [SerializeField] LevelProgressionClass[] levelProgressions = null;

    public int GetUnitsCount(int level)
    {
        return levelProgressions[level].unitsCount;
    }

    public string GetSceneName(int level)
    {
        return levelProgressions[level].sceneName;
    }

    public int GetLevelsCount()
    {
        return levelProgressions.Length;
    }

    [System.Serializable]
    public class LevelProgressionClass
    {
        public int unitsCount;
        public string sceneName;
    }
}

