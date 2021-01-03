﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public float zPosition;
    Vector3 edges;

    Mesh mesh;  

    Vector3[] vertices;
    int[] triangles;
    public Vector3 Position;


    public void Start()
    {
        edges = new Vector3(xPosition, zPosition);
    }

    private void Update()
    {
        Comunicacion.Instance.read();
        if (Comunicacion.Instance.datos_recibidos[0] != "" && Comunicacion.Instance.datos_recibidos[1] != "")
        {
            Debug.Log("READ MESH");
            xPosition = float.Parse(Comunicacion.Instance.datos_recibidos[0]); // hier gibt es das Problem
            yPosition = float.Parse(Comunicacion.Instance.datos_recibidos[1]);
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            CreateShape();
            UpdateMesh();
            Debug.Log(mesh.vertices.Length);
            mesh.vertices = vertices;
            Debug.Log(xPosition);
        }
        else
        {
            Debug.Log("SKIP MESH");
        }
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
           new Vector3 ()
        };

        triangles = new int[]
        {
            

        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(edges, .1f);

    }


    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

    }   
}
