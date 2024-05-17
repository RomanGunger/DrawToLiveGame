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

            if (saveFile._passedLevels.ContainsKey(LevelInfo.instance.CurrentChapter))
            {
                if (saveFile._passedLevels[LevelInfo.instance.CurrentChapter] < LevelInfo.instance.CurentLevel)
                {
                    saveFile._passedLevels[LevelInfo.instance.CurrentChapter] = LevelInfo.instance.CurentLevel;
                    xmlManager.Save(saveFile);
                }
            }
            else
            {
                saveFile._passedLevels.Add(LevelInfo.instance.CurrentChapter, LevelInfo.instance.CurentLevel);
                xmlManager.Save(saveFile);
            }

        }
    }
}
