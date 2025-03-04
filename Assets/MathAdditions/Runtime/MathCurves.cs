using System;
using UnityEngine;

namespace MathAdditions
{
    public static class MathCurves
    {
        public static Vector2 LissajousCurve(float A, float B, float a, float b, float delta, float t)
        {
            return new Vector2(A * Mathf.Sin(a * t + delta), B * Mathf.Sin(b * t));
        }

    }
}

