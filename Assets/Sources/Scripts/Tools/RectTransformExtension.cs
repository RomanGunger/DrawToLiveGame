using UnityEngine;

public static class RectTransformExtension
{

    public static Canvas GetCanvas(this RectTransform rt)
    {
        return rt.gameObject.GetComponentInParent<Canvas>();
    }

    public static void SetWidth(this RectTransform rt, float width)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public static void SetHeight(this RectTransform rt, float height)
    {
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height );
    }
}