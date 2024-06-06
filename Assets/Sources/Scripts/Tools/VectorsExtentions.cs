using System.Collections.Generic;
using UnityEngine;

public static class VectorsExtentions
{
    public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        return x * Vector3.Normalize(B - A) + A;
    }

    public static List<Vector2> ConvertLineToLocalPositionsList(Line currentLine, Camera camera, RectTransform rect, BoxCollider spawnPlaneCollider)
    {
        List<Vector2> newList = new List<Vector2>();

        foreach (var point in currentLine.points)
        {
            Vector2 localPos = new Vector2(point.x
                , (point.y - camera.ScreenToWorldPoint(rect.transform.position).y)
                - spawnPlaneCollider.size.z) * .5f;

            newList.Add(localPos);
        }

        return newList;
    }

    public static List<Vector2> RebuildLinePointsList(List<Vector2> list, float distanceToStackUnits)
    {
        List<Vector2> newList = new List<Vector2>();

        var tempPointsList = new List<Vector2>();
        tempPointsList.AddRange(list);
        newList.Add(tempPointsList[0]);

        Vector2 lastPoint = tempPointsList[0];

        for (int i = 1; i < tempPointsList.Count; i++)
        {
            float dist = Vector2.Distance(lastPoint, tempPointsList[i]);
            if (dist >= distanceToStackUnits)
            {
                float diff = dist - distanceToStackUnits;
                Vector3 newPoint = VectorsExtentions.LerpByDistance(tempPointsList[i], lastPoint, diff);
                lastPoint = newPoint;
                newList.Add(newPoint);
                tempPointsList.Insert(i, newPoint);
            }
        }

        return newList;
    }
}


