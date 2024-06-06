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
    public static Action<Vector3> UnitAdded;

    [SerializeField] UnitsSpawner unitsSpawner;

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
        List<Vector3> positions = DevUtils.UnitPos(UnitsList.instance.unitsList.Count
    , finishLine.GetComponent<Collider>()
    , offsetZ, offsetX, spasing
    , sizeOfUnit);

        var tasks = new List<Task>();

        for (int i = 0; i < UnitsList.instance.unitsList.Count; i++)
        {
            var operation = UnitsList.instance.unitsList[i].transform.DOMove(positions[i], 1.5f).AsyncWaitForCompletion();
            tasks.Add(operation);
            await Task.Delay(100);
        }

        await Task.WhenAll(tasks);
        LevelPassed?.Invoke();
    }

    public async void ArrangeUnitsLine(Line currentLine, RectTransform rect, Camera camera)
    {
        if (currentLine.points.Count <= 0 || UnitsList.instance.unitsList.Count <= 0)
            return;

        var tasks = new List<Task>();
        List<Vector2> rebuildedLinePointsList
            = VectorsExtentions.RebuildLinePointsList(
                VectorsExtentions.ConvertLineToLocalPositionsList(currentLine, camera, rect, spawnPlaneCollider)
                , 1f);

        int splitCount = rebuildedLinePointsList.Count / UnitsList.instance.unitsList.Count;

        //If there is more points then units
        if (splitCount > 0)
        {
            if(UnitsList.instance.unitsList.Count < rebuildedLinePointsList.Count
                && UnitsList.instance.UnitsCount > UnitsList.instance.unitsList.Count)
            {
                int unitsNeeded = rebuildedLinePointsList.Count - UnitsList.instance.unitsList.Count;
                if (unitsNeeded > UnitsList.instance.UnitsCount - UnitsList.instance.unitsList.Count)
                    unitsNeeded = UnitsList.instance.UnitsCount - UnitsList.instance.unitsList.Count;
                splitCount = rebuildedLinePointsList.Count / Mathf.Clamp(UnitsList.instance.unitsList.Count + unitsNeeded
                    ,UnitsList.instance.unitsList.Count, rebuildedLinePointsList.Count);

                UnitsList.instance.unitsList.Sort((a, b) => {
                    if (a.UnitsCount > b.UnitsCount)
                        return -1;
                    else if (a.UnitsCount < b.UnitsCount)
                        return 1;
                    else return 0;
                });

                int j = 0;
                for(int i = 0; i < unitsNeeded; i++)
                {
                    if (UnitsList.instance.unitsList[j].UnitsCount > 1)
                    {
                        unitsSpawner.AddUnit(UnitsList.instance.unitsList[j].transform.position);
                        UnitsList.instance.unitsList[j].MinusUnitsCount();

                        if (j >= UnitsList.instance.unitsList.Count)
                            j = 0;
                        if (UnitsList.instance.unitsList[j].UnitsCount <= UnitsList.instance.unitsList[j + 1].UnitsCount)
                            j++;
                    }
                }
            }

            List<List<Vector2>> slices = rebuildedLinePointsList.ChunkBy(splitCount);
            for (int i = 0; i < UnitsList.instance.unitsList.Count; i++)
            {
                var slice = slices[0];
                Vector2 midPoint;

                if (i == 0 && UnitsList.instance.unitsList.Count > 1)
                    midPoint = slice[0];
                else if (i == UnitsList.instance.unitsList.Count - 1 && UnitsList.instance.unitsList.Count > 1)
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

                tasks.Add(UnitsList.instance.unitsList[i].Rearrange(localPos));
            }
        }
        else
        {
            int j = 0;
            bool stackUnits = false;
            var listOfUnitsToRemove = new List<Unit>();

            for (int i = 0; i < UnitsList.instance.unitsList.Count; i++)
            {
                Vector3 localPos = new Vector3(rebuildedLinePointsList[j].x, 0, rebuildedLinePointsList[j].y);

                if (stackUnits)
                    listOfUnitsToRemove.Add(UnitsList.instance.unitsList[i]);

                j++;
                if (j >= rebuildedLinePointsList.Count)
                {
                    j = 0;
                    stackUnits = true;
                }
                tasks.Add(UnitsList.instance.unitsList[i].Rearrange(localPos));
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

    private void OnDestroy()
    {
        FinishLine.FinishLineReached -= OnFinishLineReached;
    }
}
