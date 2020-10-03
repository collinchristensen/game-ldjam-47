using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvertTriangles();
    }

    private void InvertTriangles()
    {

        var meshFilter = gameObject.GetComponent<MeshFilter>();
        var tris = meshFilter.sharedMesh.triangles;
        var normals = meshFilter.sharedMesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        for (int i = 0; i < tris.Length / 3; i++)
        {
            int temp = tris[i * 3 + 1];
            tris[i * 3 + 1] = tris[i * 3];
            tris[i * 3] = temp;
        }
        Mesh mesh = Instantiate(meshFilter.sharedMesh);
        mesh.triangles = tris;
        mesh.normals = normals;
        meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
