using UnityEngine;
using TMPro;

public class CurrentLevelInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        text.text = $"Level: {LevelInfo.instance.CurentLevel + 1}";
    }
}
