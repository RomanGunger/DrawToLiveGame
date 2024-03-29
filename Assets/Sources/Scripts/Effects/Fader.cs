using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class Fader : MonoBehaviour
{
    [SerializeField] Image faderImage;

    async public Task Fade(float value, float duration)
    {
        await faderImage.DOFade(value, duration).AsyncWaitForCompletion();
    }
}
