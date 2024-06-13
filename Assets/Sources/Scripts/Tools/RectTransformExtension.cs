using UnityEngine;

public static class RectTransformExtension
{

    public static Canvas GetCanvas(this RectTransform rt)
    {
        return rt.gameObject.GetComponentInParent<Canvas>();
    }

    public static float GetWidth(RectTransform rt)
    {
        return rt.rect.width;
    }

    public static float GetHeight(RectTransform rt)
    {
        return rt.rect.height;
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