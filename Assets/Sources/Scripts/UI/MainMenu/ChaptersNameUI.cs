using UnityEngine;
using TMPro;

public class ChaptersNameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        ChapterSelector.ChapterNameChanged += OnChapterNameTextChanged;
    }

    void OnChapterNameTextChanged(string name)
    {
        text.text = name;
    }
}
