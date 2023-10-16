using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distract : MonoBehaviour
{
    public Kid kid;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kid"))
        {
            kid = other.gameObject.GetComponent<Kid>();
        }
    }
}
