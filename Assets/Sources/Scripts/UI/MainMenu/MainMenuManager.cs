using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Fader fader;

    async void Start()
    {
        fader.gameObject.SetActive(true);
        await fader.Fade(0, 2f);
        fader.gameObject.SetActive(false);
    }
}
