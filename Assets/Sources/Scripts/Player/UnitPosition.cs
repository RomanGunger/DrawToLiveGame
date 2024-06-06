using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using OwnGameDevUtils;
using UnityEngine;
using static UnityEditor.Progress;

public class UnitPosition : MonoBehaviour
{
    public static Action LevelPassed;
    public static Action<Vector3> UnitAdded;

    [SerializeField] UnitsList unitsList;

    [SerializeField] Transform finishLine;

    [Header("Positioning")]
    [SerializeField] float offsetZ = 1f;
    [SerializeField] float offsetX = 1f;
    [SerializeField] float spasing = 1f;
    [SerializeField] Vector3 sizeOfUnit;

    [SerializeField] BoxCollider spawnPlaneCollider;

    private void Start()
    {
        FinishLine.FinishLineReached += OnFinishLineReached;
    }

    async void OnFinishLineReached()
    {
        List<Vector3> positions = DevUtils.UnitPos(unitsList.unitsList.Count
    , finishLine.GetComponent<Collider>()
    , offsetZ, offsetX, spasing
    , sizeOfUnit);

        var tasks = new List<Task>();

        for (int i = 0; i < unitsList.unitsList.Count; i++)
        {
            var operation = unitsList.unitsList[i].transform.DOMove(positions[i], 1.5f).AsyncWaitForCompletion();
            tasks.Add(operation);
            await Task.Delay(100);
        }

        await Task.WhenAll(tasks);
        LevelPassed?.Invoke();
    }

    public async void ArrangeUnitsLine(Line currentLine, RectTransform rect, Camera camera)
    {
        if (currentLine.points.Count <= 0 || unitsList.unitsList.Count <= 0)
            return;

        var tasks = new List<Task>();
        List<Vector2> rebuildedLinePointsList
            = VectorsExtentions.RebuildLinePointsList(
                VectorsExtentions.ConvertLineToLocalPositionsList(currentLine, camera, rect, spawnPlaneCollider)
                , 1f);

        int splitCount = rebuildedLinePointsList.Count / unitsList.unitsList.Count;

        //If there is more points then units
        if (splitCount > 0)
        {
            List<List<Vector2>> slices = rebuildedLinePointsList.ChunkBy(splitCount);
            for (int i = 0; i < unitsList.unitsList.Count; i++)
            {
                var slice = slices[0];
                Vector2 midPoint;

                if (i == 0 && unitsList.unitsList.Count > 1)
                    midPoint = slice[0];
                else if (i == unitsList.unitsList.Count - 1 && unitsList.unitsList.Count > 1)
                {
                    slice = slices[slices.Count - 1];
                    midPoint = slice[slice.Count - 1];
                }
                else
                {
                    int sliceIndex = slices.Count / 2;
                    if (sliceIndex % 2 != 0)
                        sliceIndex--;
                    slice = slices[sliceIndex];
                    midPoint = slice[slice.Count /2];
                }

                slices.Remove(slice);

                Vector3 localPos = new Vector3(midPoint.x, 0, midPoint.y);

                tasks.Add(unitsList.unitsList[i].Rearrange(localPos));
            }
        }
        else
        {
            int j = 0;
            bool stackUnits = false;
            var listOfUnitsToRemove = new List<Unit>();

            for (int i = 0; i < unitsList.unitsList.Count; i++)
            {
                Vector3 localPos = new Vector3(rebuildedLinePointsList[j].x, 0, rebuildedLinePointsList[j].y);

                if (stackUnits)
                    listOfUnitsToRemove.Add(unitsList.unitsList[i]);

                j++;
                if (j >= rebuildedLinePointsList.Count)
                {
                    j = 0;
                    stackUnits = true;
                }
                tasks.Add(unitsList.unitsList[i].Rearrange(localPos));
            }

            int unitsCount = 0;
            await Task.WhenAll(tasks);
            foreach (var item in listOfUnitsToRemove)
            {
                if (item != null)
                {
                    unitsCount += item.UnitsCount;
                    UnitsList.instance.unitsList.Remove(item);
                    Destroy(item.gameObject);
                }
            }

            UnitsList.instance.DistributeUnitsCount(unitsCount);
        }
    }

    void UnstackUnits(Line currentLine, float unitSpacement, List<Unit> unitsList)
    {
        float lineLength = 0;
        int averageUnitsCount = 0;

        for (int i = 1; i < currentLine.points.Count; i++)
        {
            lineLength += Vector3.Distance(currentLine.points[i - 1], currentLine.points[i]);
        }

        int lineCapacity = (int)(lineLength / unitSpacement);
        Debug.Log($"LineLength: {lineLength}");
        Debug.Log($"LineCapacity: {lineCapacity}");

        for(int i = 0; i < unitsList.Count; i++)
        {
            averageUnitsCount += unitsList[i].UnitsCount;
        }
        Debug.Log($"averageUnitsCount: {averageUnitsCount}");

        if (averageUnitsCount >= lineCapacity)
        {
            unitsList.Sort((a,b) => {
                if (a.UnitsCount > b.UnitsCount)
                    return -1;
                else if (a.UnitsCount < b.UnitsCount)
                    return 1;
                else return 0;
            });

            int index = 0;
            while(unitsList.Count < lineCapacity)
            {

                if (unitsList.Count > 1)
                {
                    if (unitsList[index].UnitsCount < unitsList[index + 1].UnitsCount ||
                        unitsList[index].UnitsCount <= 0 || unitsList[index] == null)
                        {
                            index++;
                        }
                }

                unitsList[index].MinusUnitsCount();
                UnitAdded?.Invoke(unitsList[index].transform.position);
            }
        }
        else
        {
            var tempUnitsList = new List<Unit>();
            tempUnitsList.AddRange(unitsList);

            foreach(var unit in tempUnitsList)
            {
                if(unit.UnitsCount > 0)
                {
                    for(int i = 0; i < unit.UnitsCount; i++)
                    {

                        unit.MinusUnitsCount();
                        UnitAdded?.Invoke(unit.transform.position);
                    }
                }
            }
        }
    }

    private async Task StackUnits(List<Task> tasks)
    {
        if (tasks.Count > 1)
        {
            var listOfUnitsToRemove = new List<Unit>();
            int currentIndex = 0;

            await Task.WhenAll(tasks);


            for (int i = 1; i < tasks.Count; i++)
            {
                for (int j = i + 1; j < tasks.Count - 1; j++)
                {
                    if (Vector3.Distance(unitsList.unitsList[i].transform.position
                         , unitsList.unitsList[j].transform.position) < .1f)
                    {
                        unitsList.unitsList[i].AddUnitsCount(unitsList.unitsList[j].UnitsCount);
                        listOfUnitsToRemove.Add(unitsList.unitsList[j]);
                        Destroy(unitsList.unitsList[j].gameObject);
                    }
                }
            }

            var list = unitsList.unitsList.Except(listOfUnitsToRemove).ToList();
            unitsList.unitsList = list;
        }
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= OnFinishLineReached;
    }
}
