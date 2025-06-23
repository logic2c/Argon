using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Mathf.Clamp01(t);
    }

    public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 q0 = Vector3.Lerp(p0, p1, t);
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(q0, q1, t);
    }
}