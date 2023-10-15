using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    Rigidbody rb;
    //Tipo de distracción u obstáculo.
    [SerializeField] Transform distraction;
    //Está o no distraído, y si tocó el trigger de la distracción o no.
    [SerializeField]bool distracted, target, pressed,land;
    [SerializeField] float speed, pickVelocity;
    Vector3 cam, currentPos, offSet;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (distracted && !pressed && land)
        {
            if(!target)
            {
                Vector3 targetDistance = (distraction.position - transform.position);
                rb.velocity = new Vector3(targetDistance.x * speed, rb.velocity.y, targetDistance.z * speed);
            }
            else
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }
        }
        else if(!pressed && land)
        {
            var p = transform.position;
            Vector3 finalVelocity = transform.parent.position - new Vector3(p.x, -rb.velocity.y, p.z);
            rb.velocity = new Vector3(finalVelocity.x * speed, rb.velocity.y, finalVelocity.z * speed);
        }
        else if (pressed)
        {
            rb.velocity = Vector3.zero;
        }
        RotateTowards();
        Touched();
    }
    void RotateTowards()
    {
        var p = transform.parent.position;
        if(distracted && rb.velocity != Vector3.zero && !pressed)
        {
            transform.LookAt(new Vector3(distraction.position.x, transform.position.y, distraction.position.z));
        }
        else if(rb.velocity != Vector3.zero && !pressed)
        {
            transform.LookAt(new Vector3(p.x,transform.position.y, p.z));
        }
    }

    public void OnMouseDown()
    {
        cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos = transform.position;
        offSet = cam - currentPos;
        pressed = true;
        land = false;
    }

    public void OnMouseUp()
    {
        pressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Iniciar animaciones
        y eventos cuando el niño 
        interactúe con la distracción*/
    }

    private void OnCollisionEnter(Collision other)
    {
        land = true;
    }

    void Touched()
    {
        if (pressed)
        {
            Vector3 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offSet;
            transform.position = new Vector3(finalPos.x, 2f, finalPos.z);
        }
    }
}
