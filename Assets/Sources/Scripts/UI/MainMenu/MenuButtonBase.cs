using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButtonBase : MonoBehaviour
{
    protected Button button;
    [SerializeField] protected AudioClip buttonSound;

    protected AudioSource audioSource;

    private void Awake()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = buttonSound;
        button.onClick.AddListener(OnClickAnimation);
        button.onClick.AddListener(OnClickAction);
    }

    protected virtual async void OnClickAnimation()
    {
        audioSource.Play();
    }

    protected virtual async void OnClickAction()
    {

    }
}
