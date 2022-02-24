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

        Vector2 position = new Vector2();
        position.x = (w[0].x * mt2) + (w[1].x * 2 * mt * t) + (w[2].x * t2);
        position.y = (w[0].y * mt2) + (w[1].y * 2 * mt * t) + (w[2].y * t2);

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
