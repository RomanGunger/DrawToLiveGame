using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Fader : MonoBehaviour
{
    Image faderImage;

    private void Awake()
    {
        faderImage = GetComponent<Image>();
    }

    async public Task Fade(float value, float duration)
    {
        await faderImage.DOFade(value, duration).SetUpdate(true).AsyncWaitForCompletion();
    }

    async public Task FadeHandle(float value, float duration, bool hideFader)
    {
        //if (value > 0.5f)
        //{
        //    Color color = faderImage.color;
        //    color.a = 0;
        //    faderImage.color = color;
        //}
        //else
        //{
        //    Color color = faderImage.color;
        //    color.a = 1;
        //    faderImage.color = color;
        //}

        gameObject.SetActive(true);
        await Fade(value, duration);

        if (hideFader)
            gameObject.SetActive(false);
    }

}
