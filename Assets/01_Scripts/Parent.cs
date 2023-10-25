using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parent : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float speed;
    [SerializeField]private bool walk;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        walk = true;
    }

    private void Update()
    {
        if (walk)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, speed);
        }
        else if (rb.velocity.z != 0f)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    private void OnMouseDown()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ignora la capa especificada (layerToIgnore) al realizar el raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (walk)
            {
                walk = false;
            }
            else
            {
                walk = true;
            }
        }
    }
}
