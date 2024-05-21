using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    Button button;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image lockImage;
    [SerializeField] LevelButtonStars levelButtonStars;

    private void Awake()
    {
        button = GetComponent<Button>();
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
