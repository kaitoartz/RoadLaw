using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shark : Distract
{
    public float jumpForce = 5.0f; 
    

    private void OnTriggerEnter(Collider other)
    {          
                // Aplica una fuerza hacia arriba para simular el salto.
                kid.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
