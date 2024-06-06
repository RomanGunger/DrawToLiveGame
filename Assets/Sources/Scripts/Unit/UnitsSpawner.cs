using System;
using System.Collections.Generic;
using OwnGameDevUtils;
using UnityEngine;

public class UnitsSpawner : MonoBehaviour
{
    public static Action LevelFailed;

    public List<GameObject> unitPrefab;
    [SerializeField] Transform spawnArea;

    [Header("Positioning")]
    [SerializeField] float offsetZ = 1f;
    [SerializeField] float offsetX = 1f;
    [SerializeField] float spasing = 1f;
    [SerializeField] Vector3 sizeOfUnit;

    private void Start()
    {
        SpawnUnits();

        BaseObstacle.UnitKilled += RemoveUnit;
        Health.UnitAdded += AddUnit;
        UnitPosition.UnitAdded += AddUnit;
    }

    public void SpawnUnits()
    {
        List<Vector3> positions = DevUtils.UnitPos(LevelInfo.instance.UnitsCount
             , spawnArea.GetComponent<Collider>()
             , 0, 0, spasing * 1.4f
             , sizeOfUnit);

        foreach (var pos in positions)
        {
            AddUnit(pos);
        }
    }

    public void AddUnit(Vector3 position)
    {
        var rnd = new System.Random();
        GameObject newUnit = Instantiate(unitPrefab[rnd.Next(0, unitPrefab.Count)], position, Quaternion.identity);

        var unit = newUnit.GetComponent<Unit>();

        newUnit.transform.localScale = new Vector3(1, 1, 1);
        newUnit.transform.SetParent(spawnArea);

        UnitsList.instance.unitsList.Add(unit);
        UnitsList.instance.UnitAdded(1);
    }

    public void RemoveUnit(Unit unit)
    {
        UnitsList.instance.unitsList.Remove(unit);

        if (UnitsList.instance.unitsList.Count <= 0)
            LevelFailed?.Invoke();
    }

    private void OnDestroy()
    {
        BaseObstacle.UnitKilled -= RemoveUnit;
        Health.UnitAdded -= AddUnit;
        UnitPosition.UnitAdded -= AddUnit;
    }
}
