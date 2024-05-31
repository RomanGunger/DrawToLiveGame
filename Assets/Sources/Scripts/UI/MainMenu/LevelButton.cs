using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelButton : MonoBehaviour
{
    Button button;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image lockImage;
    [SerializeField] LevelButtonStars levelButtonStars;

    [SerializeField] protected AudioClip buttonSound;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(async () =>
        {
            button.interactable = false;
            SoundFXManager.instance.PlaySoundFXClip(buttonSound, transform, 1f);
            await button.transform.DOPunchScale(new Vector3(-.15f, -.15f, -.15f), .2f, 0, 0)
            .SetEase(Ease.InOutBounce).AsyncWaitForCompletion();
            button.interactable = true;
        });
    }


    public void SetText(string text)
    {
        this.text.text = text;
    }

    public Button GetButton()
    {
        return button;
    }

    public void LockButton()
    {
        lockImage.gameObject.SetActive(true);
        text.gameObject.SetActive(false);
        button.interactable = false;
    }

    public void UnLockButton(int starsCount = 0)
    {
        lockImage.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        button.interactable = true;

        levelButtonStars.SetStars(starsCount);
    }
}
