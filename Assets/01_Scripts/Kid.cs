using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    Rigidbody rb;
    //Tipo de distracción u obstáculo.
    public Transform distraction;
    //Está o no distraído, y si tocó el trigger de la distracción o no.
    [SerializeField]bool target, pressed,land, isDead;
    public bool distracted;
    [SerializeField] float speed, rotSpeed;
    Vector3 cam, currentPos, offSet;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (distraction && transform.position.x == distraction.position.x && transform.position.y == distraction.position.y)
        {
            target = true;
        }
        else
        {
            target = false;
        }
        if (distracted && !pressed && land && !isDead)
        {
            if (distraction != null)
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
            else
            {
                rb.velocity = new Vector3(-1f * speed, rb.velocity.y, 0f);
            }
        }
        else if(!pressed && land && !isDead)
        {
            var p = transform.position;
            Vector3 finalVelocity = transform.parent.position - new Vector3(p.x, -rb.velocity.y, p.z);
            rb.velocity = new Vector3(finalVelocity.x * speed, rb.velocity.y, finalVelocity.z * speed);
        }
        if (pressed | isDead)
        {
            rb.velocity = Vector3.zero;
        }
        RotateTowards();
        Touched();
    }
    void RotateTowards()
    {
        if (rb.velocity != Vector3.zero)
        {
            Vector3  rot = transform.position + rb.velocity;
            Quaternion final = Quaternion.LookRotation(new Vector3(rot.x, transform.position.y, rot.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, final, rotSpeed);
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
        if (other.gameObject.CompareTag("Fila") && distracted)
        {
            distracted = false;
            target = false;
            distraction = null;
        }
        /*Iniciar animaciones
        y eventos cuando el niño 
        interactúe con la distracción*/
    }

    private void OnCollisionEnter(Collision other)
    {
        land = true;
        StartCoroutine(Run());
    }

    void Touched()
    {
        if (pressed)
        {
            Vector3 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offSet;
            transform.position = new Vector3(finalPos.x, 2f, finalPos.z);
            speed = 0f;
        }
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 2.5f;
    }
}
