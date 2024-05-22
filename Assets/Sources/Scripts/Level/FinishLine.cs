using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static Action FinishLineReached;

    [SerializeField] Collider baseCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FinishLineReached?.Invoke();
            baseCollider.enabled = false;

            XmlManager xmlManager = new XmlManager();
            SaveFile saveFile = xmlManager.Load();

            int starsCount = 2;

            Debug.Log(saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Count + " : " + LevelInfo.instance.CurentLevel);

            if (saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Count - 1 > LevelInfo.instance.CurentLevel)
                saveFile._passedLevels[LevelInfo.instance.CurrentChapter][LevelInfo.instance.CurentLevel] = starsCount;
            else
                saveFile._passedLevels[LevelInfo.instance.CurrentChapter].Add(starsCount);

            xmlManager.Save(saveFile);
        }
    }
}
