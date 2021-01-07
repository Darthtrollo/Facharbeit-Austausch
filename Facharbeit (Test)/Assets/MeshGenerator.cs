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

    private List<Vector3> Punkte = new List<Vector3>(); //Liste von den Daten erstellen
    private List<int> Dreickeeckpunkte = new List<int>(); //Liste für die Dreieckeckpunkte des Meshs

    public void Start()
    {   
        //Setup
    }



    private void Update()
    {
        Datenübertragung.Instance.read();
        if (Datenübertragung.Instance.erhaltene_Daten[1] != "" && Datenübertragung.Instance.erhaltene_Daten[2] != "")
        {
            auflösung = int.Parse(Datenübertragung.Instance.erhaltene_Daten[1]);
            abstand = float.Parse(Datenübertragung.Instance.erhaltene_Daten[3]);
            xySteps = double.Parse(Datenübertragung.Instance.erhaltene_Daten[2]);
        }

        xPosition = (float)Math.Cos((xySteps * (360.0 / 800.0)) * (Math.PI / 180.0)) * (float)(17.0/75 - abstand/75); //Cos Rechnung zur Berechnung der Koordinaten
        yPosition = (float)Math.Sin((xySteps * (360.0 / 800.0)) * (Math.PI / 180.0)) * (float)(17.0/75 - abstand/75); //Sin Rechnung zur Berechnung der Koordinaten
        zPosition = Punkte.Count / auflösung;

      
       
        vector.x = xPosition; //Daten nehmen und in Vector3 packen
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
        mesh.vertices = Punkte.ToArray();   //Liste zu Array
        mesh.triangles = Dreickeeckpunkte.ToArray(); //Liste zu Array

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.Optimize();

        GetComponent<MeshFilter>().mesh = mesh;

        return mesh;
    }


     
}





   





