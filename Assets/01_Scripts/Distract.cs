using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Distract : MonoBehaviour
{
    public Kid kid;
    public bool called;

    void Start()
    {
        transform.parent = null;
    }

    public void Update()
    {
        if (called && gameObject.CompareTag("Trap"))
        {
            gameObject.tag = "Untagged";
        }
    }

}
