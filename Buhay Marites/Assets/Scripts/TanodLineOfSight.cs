using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanodLineOfSight : MonoBehaviour
{
    public TanodFOV fov;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int rays = 50;
        float startingAngle = 90f + fov.viewAngle / 2;
        float rayAngle = fov.viewAngle / rays;

        Vector3[] vertices = new Vector3[rays + 2];
        int[] triangles = new int[rays * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rays; i++)
        {
            vertices[i + 1] = AngleToVector(startingAngle + rayAngle * -i) * (fov.radius * 1.77f);

            if (i > 0)
            {
                int tri = (i - 1) * 3;

                triangles[tri + 0] = 0;
                triangles[tri + 1] = i;
                triangles[tri + 2] = i + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = new Vector2[vertices.Length];
        mesh.triangles = triangles;
    }

    Vector3 AngleToVector(float angle)
    {
        angle *= Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
