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

    SerialPort stream = new SerialPort("COM7", 9600);
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

    public void read()
    {
        receivedstring = stream.ReadLine(); //Read the information
        stream.BaseStream.Flush(); //Clear the serial information so we assure we get new information.

        string[] datos = receivedstring.Split('|'); //My arduino script returns a 3 part value (IE: 12,30,18)
        if (datos[0] != "" && datos[1] != "" && datos[2] != "" && datos[3] != "") //Check if all values are recieved
        {
            //Debug.Log("UPDATE");
            datos_recibidos[0] = datos[0];  //1 = Programm läuft        0 = Programm kaputt
            datos_recibidos[1] = datos[1];  //auflösung in Messwerte pro Umdrehung
            datos_recibidos[2] = datos[2];  //Steps Motor XY
            datos_recibidos[3] = datos[3];  //Abstand zum Objekt in mm

            //Read the information and put it in a vector3

            //Take the vector3 and apply it to the object this script is applied.
            stream.BaseStream.Flush(); //Clear the serial information so we assure we get new information.
        }
    }
}