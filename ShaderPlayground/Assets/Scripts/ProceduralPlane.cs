using System;
using UnityEngine;

public class ProceduralPlane
{

    public static Mesh Plane(Vector2 size, Vector2Int segments)
    {
        CheckPlaneArguments(size, segments);
        Vector3[] vertices = new Vector3[(segments.x + 1) * (segments.y + 1)];
        int[] triangles = new int[segments.x * segments.y * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        Vector2 halfSize = size * 0.5f;
        Vector2 sizeStep = size / segments;
        int vi = 0;
        for (int y = 0; y < segments.y + 1; y++)
        {
            for (int x = 0; x < segments.x + 1; x++)
            {
                vertices[vi] = new Vector3(sizeStep.x * x - halfSize.x, sizeStep.y * y - halfSize.y, 0);
                uv[vi] = new Vector2(x / (float)size.x, y / (float)size.y);
                vi++;
            }
        }
        int ti = 0;
        for (int y = 0; y < segments.y; y++)
        {
            for (int x = 0; x < segments.x; x++)
            {
                triangles[ti] = x + y * (segments.x + 1);
                triangles[ti + 1] = triangles[ti + 5] = x + (y + 1) * (segments.x + 1);
                triangles[ti + 2] = triangles[ti + 4] = (x + 1) + y * (segments.x + 1);
                triangles[ti + 3] = (x + 1) + (y + 1) * (segments.x + 1);
                ti += 6;
            }
        }

        return CreateMesh(vertices, triangles, uv);
    }

    private static void CheckPlaneArguments(Vector2 size, Vector2Int segments)
    {
        if (size.x <= 0 || size.y <= 0)
        {
            throw new ArgumentException(String.Format("Size must be bigger than zero: x={0},y={1}.", size.x, size.y));
        }
        if (segments.x <= 0 || segments.y <= 0)
        {
            throw new ArgumentException(String.Format("Segments must be bigger than two: x={0},y={1}", segments.x, segments.y));
        }
    }

    private static Mesh CreateMesh(Vector3[] newVertices, int[] newTriangles, Vector2[] newUV)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = newVertices;

        mesh.uv = newUV;

        mesh.triangles = newTriangles;

        return mesh;
    }

}