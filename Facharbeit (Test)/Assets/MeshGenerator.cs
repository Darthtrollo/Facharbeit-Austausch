using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public int Reihenfolge;
    public static int xyMesswerte = 200;
    public static int zMesswerte = 200;

    Vector3 vector;
    
    Mesh mesh;

    int[] triangles;

    public float[,,] CoordinateArray = new float[zMesswerte, xyMesswerte, 2];

    //private List<Vector3> Coordinates = new List<Vector3>();
    private List<Vector3> Dreickeeckpunkte = new List<Vector3>();


    public void Start()
    {   
        //Dreickeeckpunkte[0].x
        //Koordinaten in Array einlesen
        for (int i = 0; i < zMesswerte; i++)
        {
            for (int j = 0; i < xyMesswerte; i++)
            {
                if (CoordinateArray[i, j, 0] == 0)
                {
                    CoordinateArray[i, j, 0] = xPosition;
                }                

                if (CoordinateArray[i, j, 1] == 0)
                {
                    CoordinateArray[i, j, 1] = yPosition;
                }
            }
        }

        //Liste aller Dreieckspuinkte erstellen
        for (int i = 0; i < zMesswerte; i++)
        {
            for (int j = 0; i < xyMesswerte; i++)
            {
                     
            }
        }

        //Coordinates.Add(punkte);
        //Coordinates.ToArray();        
        //Dreickeeckpunkte.Add(Reihenfolge);
        //Dreickeeckpunkte.ToArray();
    }



    private void Update()
    {
        Comunicacion.Instance.read();
        if (Comunicacion.Instance.datos_recibidos[1] != "" && Comunicacion.Instance.datos_recibidos[2] != "")
        {
            //Debug.Log("READ MESH");
            Reihenfolge = int.Parse(Comunicacion.Instance.datos_recibidos[0]);
            xPosition = float.Parse(Comunicacion.Instance.datos_recibidos[1]);
            yPosition = float.Parse(Comunicacion.Instance.datos_recibidos[2]);
            zPosition = float.Parse(Comunicacion.Instance.datos_recibidos[3]);
            //punkte = new Vector3(xPosition, yPosition, zPosition);
           
            mesh = new Mesh();
            //GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshFilter>().mesh = ConstructMesh();
            //Debug.Log(mesh.vertices.Length);
            //mesh.vertices = vertices;
            //Debug.Log(xPosition);
        }
        else
        {
            //Debug.Log("SKIP MESH");
        }
    }




   /* void CreateShape()
    {
        vertices = new Vector3[]
        {
          edges
        };

        triangles = new int[]
        {
            Reihenfolge

        };
    }*/
    public Mesh ConstructMesh()
    {
        Mesh mesh = new Mesh
        {
            //vertices = Coordinates.ToArray(),
            //triangles = Dreickeeckpunkte.ToArray()
    };

        return mesh;
    }



}





   





