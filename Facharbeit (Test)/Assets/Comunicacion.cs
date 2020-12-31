using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Comunicacion : MonoBehaviour
{

    #region Instance
    private static Comunicacion instance;
    public static Comunicacion Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Comunicacion>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned GyroManager", typeof(Comunicacion)).GetComponent<Comunicacion>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }

    }
    #endregion

    SerialPort stream = new SerialPort("COM3", 9600);
    public string receivedstring;
    public GameObject carrito;
    public Vector3 rot;
    public Vector3 rot2;
    public string[] datos;
    public string[] datos_recibidos;


    void Start()
    {
        stream.Open(); //Open the Serial Stream.
    }

    void Update()
    {
        receivedstring = stream.ReadLine(); //Read the information
        stream.BaseStream.Flush(); //Clear the serial information so we assure we get new information.

        string[] datos = receivedstring.Split('|'); //My arduino script returns a 3 part value (IE: 12,30,18)
        if (datos[0] != "" && datos[1] != "" && datos[2] != "") //Check if all values are recieved
        {
            datos_recibidos[0] = datos[0];
            datos_recibidos[1] = datos[1];
            datos_recibidos[2] = datos[2];


            //Read the information and put it in a vector3

            //Take the vector3 and apply it to the object this script is applied.
            stream.BaseStream.Flush(); //Clear the serial information so we assure we get new information.
        }
    }
}