using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Distract
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            Destroy(gameObject);
        }
    }
}
