using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    Rigidbody rb;
    bool detectarMouse;
    //Tipo de distracción u obstáculo.
    [SerializeField] Transform distraction;
    //Está o no distraído, y si tocó el trigger de la distracción o no.
    [SerializeField]bool distracted, target;
    [SerializeField] float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(distracted)
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
            Vector3 finalVelocity = transform.parent.position - new Vector3(transform.position.x, -rb.velocity.y, transform.position.z);
            rb.velocity = new Vector3(finalVelocity.x * speed, rb.velocity.y, finalVelocity.z * speed);
        }
        RotateTowards();
    }
    void RotateTowards()
    {
        if(distracted && rb.velocity != Vector3.zero)
        {
            transform.LookAt(new Vector3(distraction.position.x, transform.position.y, distraction.position.z));
        }
        else if(rb.velocity != Vector3.zero)
        {
            transform.LookAt(transform.parent.position + new Vector3(0f,transform.position.y, 0f));
        }
    }
    private void OnTriggerEnter()
    {
        /*Iniciar animaciones
        y eventos cuando el niño 
        interactúe con la distracción*/
    }
}
