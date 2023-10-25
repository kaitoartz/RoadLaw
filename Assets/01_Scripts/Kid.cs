using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
     public Rigidbody rb;
    //Tipo de distracción u obstáculo.
    public Transform distraction;
    //Está o no distraído, y si tocó el trigger de la distracción o no, isDead es si el niño está muerto.
    public bool distracted, isDead;
    //Si el niño esta en tierra, Si el niño ha sido clickeado.
    private bool land, pressed;
    [SerializeField] float speed, rotSpeed;
    Vector3 cam, currentPos, offSet;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //niño está distraído.
        if (distracted && !pressed && land && !isDead)
        {
            if (distraction != null)
            {
                
                //Al llegar a la distracción.
               
                
                    Vector3 targetDistance = (distraction.position - transform.position);
                    rb.velocity = new Vector3(targetDistance.x * 1.5f, rb.velocity.y, targetDistance.z * 1.5f);
                

                
            }
            //Al no tener asignada una distracción.
            else
            {
                rb.velocity = new Vector3(-1f * speed, rb.velocity.y, 0f);
            }
        }
        //El niño no está distraído
        else if(!pressed && land && !isDead)
        {
            Vector3 p = transform.parent.position - transform.position;
            rb.velocity = new Vector3(p.x * speed, rb.velocity.y, p.z * speed);
        }
        //En caso de muerte, levantar al niño o Pausa (Vos Pipo ya sabí).
        if (pressed | isDead)
        {
            rb.velocity = Vector3.zero;
        }
        //Setear rotación y detector de clicks.
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
    //Levantar al niño.
    public void OnMouseDown()
    {
        cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos = transform.position;
        offSet = cam - currentPos;
        pressed = true;
        land = false;
    }
    //Bajar al niño.
    public void OnMouseUp()
    {
        pressed = false;
    }
    //Al colisionar con Triggers.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fila") && distracted)
        {
            print("ok");
            distracted = false;
            distraction = null;
        }
    }
    //Al colisionar con físicas.
    private void OnCollisionEnter(Collision other)
    {
        land = true;
        StartCoroutine(Run());
    }
    //Detectar Mouse;
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
