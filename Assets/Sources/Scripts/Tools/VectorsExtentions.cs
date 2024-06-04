using UnityEngine;

public static class VectorsExtentions
{
    public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        return x * Vector3.Normalize(B - A) + A;
    }
}
