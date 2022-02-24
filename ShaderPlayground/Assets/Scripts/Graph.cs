using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    [Range(4, 200)]
    private int drawnPointCount;
    private GameObject[] drawnPoints;

    private int pointCount = 4;
    private Transform[] points;
    private Vector2[] weights;
    
    [SerializeField]
    private GameObject pointPrefab;

    void OnEnable()
    {
        if (drawnPointCount <= 0) { drawnPointCount = 5; }

        GeneratePoints();
        GenerateGraph();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearPrevious();
            GenerateGraph();
        }
    }

    private void ClearPrevious()
    {
        for (int i = 0; i < drawnPoints.Length; i++)
        {
            Destroy(drawnPoints[i]);
        }
    }

    private void GeneratePoints()
    {
        points = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            GameObject p = Instantiate(pointPrefab, this.transform);
            p.name = "point" + i.ToString();

            p.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

            points[i] = p.transform;
        }

        weights = new Vector2[pointCount];
        UpdateWeights();
    }

    private void GenerateGraph()
    {
        drawnPoints = new GameObject[drawnPointCount];
        UpdateWeights();

        float t = 0;
        float interval = 1 / ((float)drawnPointCount - 1);
        for (int i = 0; i < drawnPointCount; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            Vector2 position = BezierCurves.BezierCubic(t, weights);
            go.transform.position = position;
            
            drawnPoints[i] = go;

            Debug.Log("t = " + t + ".  x = " + position.x + ".  y = " + position.y);

            t += interval;
        }
    }

    private void UpdateWeights()
    {
        for (int i = 0; i < pointCount; i++)
        {
            weights[i].x = points[i].position.x;
            weights[i].y = points[i].position.y;
        }
    }

}
