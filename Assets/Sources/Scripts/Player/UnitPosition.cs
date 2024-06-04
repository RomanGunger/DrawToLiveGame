using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using OwnGameDevUtils;
using UnityEngine;

public class UnitPosition : MonoBehaviour
{
    public static Action LevelPassed;

    [SerializeField] UnitsList unitsList;

    [SerializeField] Transform finishLine;
    [SerializeField] Camera camera;

    float distanceToStackUnit = .5f;

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

    public async void ArrangeUnitsLine(Line currentLine, RectTransform rect)
    {
        var tasks = new List<Task>();

        if (currentLine.PointsCount >= 1 && unitsList.unitsList.Count > 0)
        {
            int splitCount = currentLine.points.Count / unitsList.unitsList.Count;

            //If there is more points then units
            if(splitCount > 0)
            {
                List<List<Vector2>> slices = new List<List<Vector2>>();

                slices = currentLine.points.ChunkBy(splitCount);

                //If we have only one unit, we put him in the middle of the line
                if (unitsList.unitsList.Count <= 1)
                {
                    var slice = slices[slices.Count / 2];
                    Vector2 midPoint = slice[slice.Count / 2];


                    Vector3 localPos = new Vector3(midPoint.x * 0.5f
                        , 0
                        , (midPoint.y - camera.ScreenToWorldPoint(rect.transform.position).y
                        - spawnPlaneCollider.size.z) * .5f);

                    unitsList.unitsList[0].Rearrange(localPos);

                    return;
                }

                for (int i = 0; i < unitsList.unitsList.Count; i++)
                {
                    var slice = slices[0];
                    Vector2 midPoint;

                    if (i == 0)
                        midPoint = slice[0];
                    else if (i == unitsList.unitsList.Count - 1)
                        midPoint = slice[slice.Count - 1];
                    else
                        midPoint = slice[slice.Count / 2];

                    slices.RemoveAt(0);

                    Vector3 localPos = new Vector3(midPoint.x * 0.5f
                        , 0
                        , (midPoint.y - camera.ScreenToWorldPoint(rect.transform.position).y
                        - spawnPlaneCollider.size.z) * .5f);

                    //unitsList.unitsList[i].Rearrange(localPos);
                    tasks.Add(unitsList.unitsList[i].Rearrange(localPos));
                    StackUnits(tasks);
                }
            }
            else
            {
                int j = 0;

                for (int i = 0; i < unitsList.unitsList.Count; i++)
                {
                    Vector3 localPos = new Vector3(currentLine.points[j].x * 0.5f
                        , 0
                        , (currentLine.points[j].y - camera.ScreenToWorldPoint(rect.transform.position).y
                        - spawnPlaneCollider.size.z) * .5f);

                    j++;

                    if (j >= currentLine.points.Count)
                        j = 0;

                    tasks.Add(unitsList.unitsList[i].Rearrange(localPos));
                }

                StackUnits(tasks);
            }

        }
    }

    private async Task StackUnits(List<Task> tasks)
    {
        if (tasks.Count > 1)
        {
            var listOfUnitsToRemove = new List<Unit>();
            int currentIndex = 0;
            for (int i = 1; i < tasks.Count; i++)
            {
                var tasksTemp = new List<Task>()
                        {
                            tasks[i-1],
                            tasks[i]
                        };

                await Task.WhenAll(tasksTemp);

                if (Vector3.Distance(unitsList.unitsList[currentIndex].transform.position
                    , unitsList.unitsList[i].transform.position) < distanceToStackUnit)
                {
                    unitsList.unitsList[currentIndex].SetUnitsCount(unitsList.unitsList[i].UnitsCount);
                    listOfUnitsToRemove.Add(unitsList.unitsList[i]);
                    Destroy(unitsList.unitsList[i].gameObject);
                }
                else
                {
                    currentIndex = i;
                    Debug.Log($"current index: {currentIndex}");
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
