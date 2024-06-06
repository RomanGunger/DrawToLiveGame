using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;

    public List<Vector2> points = new List<Vector2>();

    int pointsCount = 0;
    [HideInInspector] public int PointsCount { get { return pointsCount; } }

    [HideInInspector] public Dictionary<GameObject, bool> obstacles = new Dictionary<GameObject, bool>();

    float pointsMinDistance = .1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AddPoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
        {
            return;
        }


        points.Add(newPoint);
        pointsCount++;

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
    }

    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void FillLineGaps()
    {
        for(int i = 0; i >= points.Count - 1; i++)
        {
            if (Vector2.Distance(points[i], points[i+1]) > pointsMinDistance)
            {
                points.Add(VectorsExtentions.LerpByDistance(points[i], points[i + 1], .5f));
            }
        }
    }

    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
