using UnityEngine;
using TMPro;

public class UnitsCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        UnitsList.UnitsCountChanged += OnUnitCountChanged;
    }

    void OnUnitCountChanged(int unitsCount)
    {
        text.text = unitsCount.ToString();
    }

    private void OnDestroy()
    {
        UnitsList.UnitsCountChanged -= OnUnitCountChanged;
    }
}
