using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;  // Velocidad del vehículo hacia adelante.

    private void Start()
    {
        // Aplica una fuerza hacia adelante al RigidBody del vehículo para hacerlo moverse.
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el vehículo colisiona con un objeto que no sea la calle.
        if (!collision.gameObject.CompareTag("Street"))
        {
            // Destruye el vehículo cuando deja de colisionar con la calle.
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
