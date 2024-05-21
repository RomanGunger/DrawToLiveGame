using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonStars : MonoBehaviour
{
    [SerializeField] List<Image> starImages;
    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite uncativeStar;

    private void Start()
    {
        //foreach (var item in starImages)
        //{
        //    item.sprite = uncativeStar;
        //}
    }

    public void SetStars(int starsCount = 0)
    {
        if (starsCount > starImages.Count || starsCount == 0)
            return;

        for(int i = 0; i < starsCount; i++)
        {
            starImages[i].sprite = activeStar;
        }
    }
}
