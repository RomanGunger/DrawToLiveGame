using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UnitsList : MonoBehaviour
{
    public static UnitsList instance;

    public static Action<int> UnitsCountChanged;

    public List<Unit> unitsList = new List<Unit>();
    public int UnitsCount { get; private set; }

    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        UnitsCount = LevelInfo.instance.UnitsCount;
        UnitsCountChanged?.Invoke(UnitsCount);
    }

    public void DistributeUnitsCount(int unitsCount)
    {
        int j = 0;
        for(int i = 0; i < unitsCount; i++)
        {
            unitsList[j].AddUnitsCount(1);
            j++;

            if (j >= unitsList.Count)
                j = 0;
        }
    }

    public void UnitKilled(int unitsCount)
    {
        UnitsCount -= unitsCount;
        UnitsCountChanged?.Invoke(UnitsCount);
    }

    public void UnitAdded(int unitsCount)
    {
        UnitsCount += unitsCount;
        UnitsCountChanged?.Invoke(UnitsCount);
    }

    private void Update()
    {
        text.text = UnitsCount.ToString();
    }
}
