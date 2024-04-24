using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using OwnGameDevUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitPosition : MonoBehaviour
{
    public static Action LevelFailed;
    public static Action<List<Unit>> LevelPassed;

    public int initialCount = 10;

    [SerializeField] Transform spawnArea;
    [SerializeField] Transform finishLine;
    [SerializeField] Camera camera;

    [HideInInspector] public List<Unit> units = new List<Unit>();

    public List<GameObject> unitPrefab;

    [Header("Positioning")]
    [SerializeField] float offsetZ = 1f;
    [SerializeField] float offsetX = 1f;
    [SerializeField] float spasing = 1f;
    [SerializeField] Vector3 sizeOfUnit;

    private void Start()
    {
        FinishLine.FinishLineReached += ArrangeUnitsFinishLine;

        SpawnUnits();
    }

    void SpawnUnits()
    {
       List<Vector3> positions = DevUtils.UnitPos(initialCount
            , spawnArea.GetComponent<Collider>()
            , 0, 0, spasing * 1.4f
            , sizeOfUnit);

        foreach (var pos in positions)
        {
            AddUnit(pos);
        }
    }

    async void ArrangeUnitsFinishLine()
    {
        List<Vector3> positions = DevUtils.UnitPos(units.Count
    , finishLine.GetComponent<Collider>()
    , offsetZ, offsetX, spasing
    , sizeOfUnit);

        var tasks = new List<Task>();

        for (int i = 0; i < units.Count; i++)
        {
            tasks.Add(units[i].transform.DOMove(positions[i], 1.5f).AsyncWaitForCompletion());
            await Task.Delay(100);
        }

        await Task.WhenAll(tasks);
        LevelPassed?.Invoke(units);
    }

    public void ArrangeUnitsLine(Line currentLine, RectTransform rect)
    {
        if (currentLine.PointsCount >= 1 && units.Count > 0)
        {
            int splitCount = currentLine.points.Count / units.Count;

            if(splitCount > 0)
            {
                List<List<Vector2>> slices = new List<List<Vector2>>();

                slices = currentLine.points.ChunkBy(splitCount);

                foreach (var unit in units)
                {
                    var slice = slices[0];
                    Vector2 midPoint = slice[slice.Count / 2];
                    slices.RemoveAt(0);

                    Vector3 localPos = new Vector3(midPoint.x * 0.5f
                        , 0
                        , midPoint.y - camera.ScreenToWorldPoint(rect.transform.position).y);

                    unit.Rearrange(localPos);
                }
            }
            else
            {
                int i = 0;
                foreach (var unit in units)
                {
                    Vector3 localPos = new Vector3(currentLine.points[i].x * 0.5f
                        , 0
                        , currentLine.points[i].y - camera.ScreenToWorldPoint(rect.transform.position).y);

                    i++;
                    if (i >= currentLine.points.Count)
                        i = 0;

                    unit.Rearrange(localPos);
                }
            }

        }
    }

    public void AddUnit(Vector3 position)
    {
        var rnd = new System.Random();
        GameObject newUnit = Instantiate(unitPrefab[rnd.Next(0, unitPrefab.Count)], position, Quaternion.identity);

        var unit = newUnit.GetComponent<Unit>();
        unit.unitPosition = this;

        newUnit.transform.localScale = new Vector3(1,1,1);
        newUnit.transform.SetParent(spawnArea);

        units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        units.Remove(unit);

        if (units.Count <= 0)
            LevelFailed?.Invoke();
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= ArrangeUnitsFinishLine;
    }
}
