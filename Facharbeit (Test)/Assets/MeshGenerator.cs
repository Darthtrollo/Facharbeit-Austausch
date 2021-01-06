using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public double xySteps;
    public float abstand;
    public int auflösung;

    public Vector3 vector;
    
    Mesh mesh;

    private List<Vector3> Punkte = new List<Vector3>();
    private List<int> Dreickeeckpunkte = new List<int>();

    public void Start()
    {   
        //Setup
    }



    private void Update()
    {
        Comunicacion.Instance.read();
        if (Comunicacion.Instance.datos_recibidos[1] != "" && Comunicacion.Instance.datos_recibidos[2] != "")
        {
            auflösung = int.Parse(Comunicacion.Instance.datos_recibidos[1]);
            abstand = float.Parse(Comunicacion.Instance.datos_recibidos[3]);
            xySteps = double.Parse(Comunicacion.Instance.datos_recibidos[2]);
        }

        xPosition = (float)Math.Cos((xySteps * (360.0 / 800.0)) * (Math.PI / 180.0)) * (float)(17.0/75 - abstand/75);
        yPosition = (float)Math.Sin((xySteps * (360.0 / 800.0)) * (Math.PI / 180.0)) * (float)(17.0/75 - abstand/75);
        zPosition = Punkte.Count / auflösung;

        Debug.Log(xPosition);
        Debug.Log(yPosition);

        vector.x = xPosition;
        vector.y = zPosition;
        vector.z = yPosition;

        Punkte.Add(vector);

        if (Punkte.Count > auflösung) {
            Dreickeeckpunkte.Clear();
            for (int i = 0; i < Punkte.Count - 1 - auflösung; i++)
            {
                Dreickeeckpunkte.Add( i );
                Dreickeeckpunkte.Add( (i + 1) % auflösung + (i / auflösung) * auflösung );
                Dreickeeckpunkte.Add( ((i + 1) % auflösung + (i / auflösung) * auflösung) + auflösung );

                Dreickeeckpunkte.Add(i + auflösung);
                Dreickeeckpunkte.Add( i );
                Dreickeeckpunkte.Add(((i + 1) % auflösung + (i / auflösung) * auflösung) + auflösung);
            }
        }

        GetComponent<MeshFilter>().mesh = ConstructMesh();
    }


    public Mesh ConstructMesh()
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = Punkte.ToArray();
        mesh.triangles = Dreickeeckpunkte.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.Optimize();

        GetComponent<MeshFilter>().mesh = mesh;

        return mesh;
    }


     
}





   





