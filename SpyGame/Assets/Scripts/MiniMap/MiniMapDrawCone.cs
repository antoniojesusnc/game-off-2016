using UnityEngine;
using System.Collections;
using Geometry;
using System;

public class MiniMapDrawCone : MonoBehaviour
{

    public int _quality = 15;
    public Cone _cone;

    public Material _sharedMaterial;
    private Mesh _mesh;

    void Start()
    {
        Init();
    } // Start

    void Init()
    {
        GenerateMesh();
        // setting the calculate mesh to the object
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshRenderer>().sharedMaterial = _sharedMaterial;
    } // Init

    /// <summary>
    /// Generate the Cone, first creating the pieces that not depends of the Cone and after
    /// setting the pieces that depends of the cone properties
    /// </summary>
    public void GenerateMesh()
    {
        _mesh = new Mesh();
        _mesh.vertices = new Vector3[3 * _quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
        _mesh.triangles = new int[3 * _quality];

        Vector3[] normals = new Vector3[3 * _quality];
        Vector2[] uv = new Vector2[3 * _quality];

        for (int i = 0; i < uv.Length; i++)
        {
            uv[i++] = new Vector2(0, 0);
            uv[i++] = new Vector2(1, 1);
            uv[i] = new Vector2(0, 1);
        }
        for (int i = 0; i < normals.Length; i++)
            normals[i] = new Vector3(0, 1, 0);

        _mesh.uv = uv;
        _mesh.normals = normals;
        SetMeshPoperties();
    } // GenerateMesh

    /// <summary>
    /// Set the values for the Cone that depends of the Cone
    /// </summary>
    private void SetMeshPoperties()
    {
        float halfConeAngle = _cone.getAngle() * 0.5f;
        float eachAngle = _cone.getAngle() / _quality;

        Vector3 point1;
        Vector3 point2;

        point1 = Quaternion.Euler(0, -halfConeAngle, 0) * transform.forward;
        point1 *= _cone.getLength();
        point1 += transform.position;

        var vertices = new Vector3[4 * _quality];
        var triangles = new int[3 * 2 * _quality];

        int index = 0;
        for (float i = -halfConeAngle + eachAngle; i <= halfConeAngle; i += eachAngle, ++index)
        {
            point2 = Quaternion.Euler(0, i, 0) * transform.forward;
            point2 *= _cone.getLength();
            point2 += transform.position;

            vertices[index * 3] = transform.worldToLocalMatrix.MultiplyPoint3x4(transform.position);
            vertices[index * 3 + 1] = transform.worldToLocalMatrix.MultiplyPoint3x4(point1);
            vertices[index * 3 + 2] = transform.worldToLocalMatrix.MultiplyPoint3x4(point2);

            triangles[index * 3] = index * 3;       // Triangle1: abc
            triangles[index * 3 + 1] = index * 3 + 1;
            triangles[index * 3 + 2] = index * 3 + 2;
            //Vector3 dir = Quaternion.Euler(0, i, 0) * transform.forward;

            point1 = point2;
        }
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
    } // SetMeshPoperties
}
