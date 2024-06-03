using System;
using System.Collections.Generic;
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

    public void ArrangeUnitsLine(Line currentLine, RectTransform rect)
    {
        if (currentLine.PointsCount >= 1 && unitsList.unitsList.Count > 0)
        {
            int splitCount = currentLine.points.Count / unitsList.unitsList.Count;

            if(splitCount > 0)
            {
                List<List<Vector2>> slices = new List<List<Vector2>>();

                slices = currentLine.points.ChunkBy(splitCount);

                foreach (var unit in unitsList.unitsList)
                {
                    var slice = slices[0];
                    Vector2 midPoint = slice[slice.Count / 2];
                    slices.RemoveAt(0);

                    Vector3 localPos = new Vector3(midPoint.x * 0.5f
                        , 0
                        , (midPoint.y - camera.ScreenToWorldPoint(rect.transform.position).y
                        - spawnPlaneCollider.size.z) * .5f);

                    unit.Rearrange(localPos);
                }
            }
            else
            {
                int i = 0;
                foreach (var unit in unitsList.unitsList)
                {
                    Vector3 localPos = new Vector3(currentLine.points[i].x * 0.5f
                        , 0
                        , (currentLine.points[i].y - camera.ScreenToWorldPoint(rect.transform.position).y
                        - spawnPlaneCollider.size.z) * .5f);

                    i++;
                    if (i >= currentLine.points.Count)
                        i = 0;

                    unit.Rearrange(localPos);
                }
            }

        }
    }

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= OnFinishLineReached;
    }
}
