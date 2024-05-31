using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButtonBase : MonoBehaviour
{
    protected Button button;
    [SerializeField] protected AudioClip buttonSound;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickAnimation);
        button.onClick.AddListener(OnClickAction);
    }

    protected virtual async void OnClickAnimation()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSound, transform, 1f);
    }

    protected virtual async void OnClickAction()
    {

    }
}
