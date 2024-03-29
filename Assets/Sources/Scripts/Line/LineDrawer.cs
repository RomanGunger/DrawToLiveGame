using UnityEngine;
using OwnGameDevUtils;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    [SerializeField] RectTransform rect;
    [SerializeField] UnitPosition unitPosition;
    [SerializeField] Camera camera;

    [Space(30f)]
    [SerializeField] Gradient lineColor;
    [SerializeField] float linePointsMinDistance;
    [SerializeField] float lineWidth;

    bool canDraw = false;
 
    Line currentLine;

    private void Start()
    {
        GameConditions.LevelStarted += CanDraw;
        FinishLine.FinishLineReached += CanDraw;
        UnitPosition.LevelFailed += CanDraw;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();

    }

    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, transform).GetComponent<Line>();

        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    void Draw()
    {
        Vector2 mousePosition = DevUtils.GetMouseWorldPosition(camera);

        if (DevUtils.MouseRect(rect, Input.mousePosition) && canDraw)
            currentLine.AddPoint(mousePosition);

    }

    void EndDraw()
    {
        if(currentLine != null)
        {
            unitPosition.ArrangeUnitsLine(currentLine, rect);
            Destroy(currentLine.gameObject);
            currentLine = null;

        }
    }

    void CanDraw()
    {
        canDraw = canDraw ? false : true;
    }

    private void OnDestroy()
    {
        GameConditions.LevelStarted -= CanDraw;
        FinishLine.FinishLineReached -= CanDraw;
        UnitPosition.LevelFailed -= CanDraw;
    }
}
