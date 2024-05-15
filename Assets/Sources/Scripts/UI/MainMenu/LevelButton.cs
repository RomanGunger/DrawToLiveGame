using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    Button button;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image lockImage;

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
        button.interactable = false;
    }
}
