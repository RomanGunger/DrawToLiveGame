using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsWinUI : MonoBehaviour
{
    [SerializeField] List<Image> starImages;
    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite uncativeStar;
    [SerializeField] StarsHandler starsHandler;

    private void OnEnable()
    {
        SetStars(starsHandler.Stars);
    }

    public void SetStars(int starsCount = 0)
    {
        if (starsCount > starImages.Count || starsCount == 0)
            return;

        for (int i = 0; i < starsCount; i++)
        {
            starImages[i].sprite = activeStar;
        }
    }
}
