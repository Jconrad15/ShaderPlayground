using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierCurves
{
    public static Vector2 BezierQuadratic(float t, Vector2[] w)
    {
        float t2 = t * t;
        float mt = 1 - t;
        float mt2 = mt * mt;

        Vector2 position = new Vector2
        {
            x = (w[0].x * mt2) + (w[1].x * 2 * mt * t) + (w[2].x * t2),
            y = (w[0].y * mt2) + (w[1].y * 2 * mt * t) + (w[2].y * t2)
        };

        return position;
    }

    public static Vector2 BezierCubic(float t, Vector2[] w)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        float mt = 1 - t;
        float mt2 = mt * mt;
        float mt3 = mt2 * mt;

        Vector2 position = new Vector2
        {
            x = (w[0].x * mt3) +
                (3 * w[1].x * mt2 * t) +
                (3 * w[2].x * mt * t2) +
                (w[3].x * t3),

            y = (w[0].y * mt3) +
                (3 * w[1].y * mt2 * t) +
                (3 * w[2].y * mt * t2) +
                (w[3].y * t3)
        };

        return position;
    }

    public static float BezierCubicArcLength(Vector2[] w, int points = 100)
    {
        if (points <= 0) { points = 5; }

        float t = 0;
        float interval = 1 / ((float)points - 1);
        float distance = 0;

        Vector2 prevPosition = new Vector2();
        for (int i = 0; i < points; i++)
        {
            Vector2 currPosition = BezierCurves.BezierCubic(t, w);

            if (i != 0)
            {
                distance += Vector2.Distance(currPosition, prevPosition);
            }

            prevPosition = currPosition;
            t += interval;
        }

        return distance;
    }

    public static float[] BezierCubicLengthTable(Vector2[] w, int points)
    {
        float t = 0;
        float interval = 1 / ((float)points - 1);
        float distance = 0;
        float[] lengthTable = new float[points];
        lengthTable[0] = 0f;

        Vector2 prevPosition = new Vector2();
        for (int i = 0; i < points; i++)
        {
            Vector2 currPosition = BezierCurves.BezierCubic(t, w);

            if (i != 0)
            {
                distance += Vector2.Distance(currPosition, prevPosition);
                lengthTable[i] = distance;
            }

            prevPosition = currPosition;
            t += interval;
        }

        return lengthTable;
    }

    public static float BezierCubicSampleFromTable(float[] lengthTable, float t)
    {
        int count = lengthTable.Length;
        if (count == 0)
        {
            Debug.LogError("Unable to sample array - it has no elements");
            return 0;
        }

        if (count == 1) { return lengthTable[0]; }

        float iFloat = t * (count - 1);
        int idLower = Mathf.FloorToInt(iFloat);
        int idUpper = Mathf.FloorToInt(iFloat + 1);

        if (idUpper >= count) { return lengthTable[count - 1]; }
        if (idLower < 0) { return lengthTable[0]; }

        return Mathf.Lerp(lengthTable[idLower], lengthTable[idUpper], iFloat - idLower);
    }

    public static Vector2 RationalBezierQuadratic(float t, Vector2[] w, float[] r)
    {
        float t2 = t * t;
        float mt = 1 - t;
        float mt2 = mt * mt;

        float[] f = new float[] 
        { 
            r[0] * mt2,
            r[1] * 2 * mt * t,
            r[2] * t2 
        };

        float basis = f[0] + f[1] + f[2];

        Vector2 position = new Vector2
        {
            x = (f[0] * w[0].x + f[1] * w[1].x + f[2] * w[2].x) / basis,
            y = (f[0] * w[0].y + f[1] * w[1].y + f[2] * w[2].y) / basis
        };

        return position;
    }

    public static Vector2 RationalBezierCubic(float t, Vector2[] w, float[] r)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        float mt = 1 - t;
        float mt2 = mt * mt;
        float mt3 = mt2 * mt;

        float[] f = new float[]
        {
            r[0] * mt3,
            3 * r[1] * mt2 * t,
            3 * r[2] * mt * t2,
            r[3] * t3
        };

        float basis = f[0] + f[1] + f[2] + f[3];

        Vector2 position = new Vector2
        {
            x = (f[0] * w[0].x + f[1] * w[1].x + f[2] * w[2].x + f[3] * w[3].x) / basis,
            y = (f[0] * w[0].y + f[1] * w[1].y + f[2] * w[2].y + f[3] * w[3].y) / basis
        };

        return position;
    }

}
