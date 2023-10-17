using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour
{
    public bool estaRojo = false;
    private float speed = 10f;


    // Lista para almacenar los autos que se mueven en el eje Z
    public List<GameObject> autosEjeZ = new List<GameObject>();

    // Lista para almacenar los autos que se mueven en el eje X
    public List<GameObject> autosEjeX = new List<GameObject>();

    void Update()
    {
        // Verifica el estado del semáforo y detiene o permite que los autos se muevan según sea necesario
        if (estaRojo)
        {
            DetenerAutos(autosEjeZ);
            DetenerAutos(autosEjeX);
        }
        else
        {
            PermitirAutos(autosEjeZ);
            PermitirAutos(autosEjeX);
        }
    }

    void DetenerAutos(List<GameObject> autos)
    {
        foreach (GameObject auto in autos)
        {
            auto.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void PermitirAutos(List<GameObject> autos)
    {
        foreach (GameObject auto in autos)
        {
            auto.GetComponent<Rigidbody>().velocity = auto.transform.forward * speed; // Ajusta la velocidad según sea necesario
        }
    }
}
