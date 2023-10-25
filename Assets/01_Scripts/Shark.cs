using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shark : Distract
{
    public float jumpForce = 5.0f; 
    public Kid kid;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid") && other.GetComponent<Kid>().target)
        {
            Rigidbody kidRigidbody = other.GetComponent < Rigidbody();

            if (kidRigidbody != null)
            {
                // Aplica una fuerza hacia arriba para simular el salto.
                kidRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
