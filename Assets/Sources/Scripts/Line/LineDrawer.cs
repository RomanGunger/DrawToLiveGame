using UnityEngine;
using OwnGameDevUtils;
using System.Threading.Tasks;

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

    [SerializeField]Line currentLine;

    private void Start()
    {
        GameConditions.LevelStarted += CanDraw;
        FinishLine.FinishLineReached += CanDraw;
        UnitPosition.LevelFailed += CanDraw;
    }


    //void TouchInput()
    //{
    //    if (Input.touchCount == 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        BeginDraw();

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            Vector2 pos = touch.position;
    //            Draw(pos);
    //        }

    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            EndDraw();
    //        }
    //    }
    //}

    private void Update()
    {
        MouseInput();

        //TouchInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw(DevUtils.GetMouseWorldPosition(camera));

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    void BeginDraw()
    {
        if (canDraw && currentLine == null)
        {
            currentLine = Instantiate(linePrefab, transform).GetComponent<Line>();

            currentLine.SetLineColor(lineColor);
            currentLine.SetPointsMinDistance(linePointsMinDistance);
            currentLine.SetLineWidth(lineWidth);
        }

    }

    void Draw(Vector2 pos)
    {
        if (DevUtils.MouseRect(rect, DevUtils.GetTouchWorldPosition(camera, pos)) && canDraw)
            currentLine.AddPoint(pos);

        if (DevUtils.MouseRect(rect, Input.mousePosition) && canDraw)
            currentLine.AddPoint(pos);
    }

    void EndDraw()
    {
        if (currentLine != null)
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
