using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private Material planeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlaneGameObject();
    }

    private void CreatePlaneGameObject()
    {
        GameObject plane = new GameObject("Plane");
        plane.transform.SetParent(gameObject.transform);

        // Create Mesh Renderer
        MeshRenderer mr = plane.AddComponent<MeshRenderer>();
        mr.material = planeMaterial;

        // Create Mesh Filter
        MeshFilter mf = plane.AddComponent<MeshFilter>();
        Vector2 size = new Vector2(10, 10);
        Vector2Int segments = new Vector2Int(20, 20);
        mf.mesh = ProceduralPlane.Plane(size, segments);
    }
}
