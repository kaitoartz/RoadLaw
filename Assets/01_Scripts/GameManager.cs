using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Kid[] kids;
    private int index, probabilityForKid;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    //Inicia Corrutina;
    void Start()
    {
        StartCoroutine(DistractingKids());
    }
    //Define cada cuánto tiempo un niño podría distraerse.
    IEnumerator DistractingKids()
    {
        probabilityForKid = UnityEngine.Random.Range(0, 3);
        yield return new WaitForSeconds(2f);
        ConfirmDistraction();
    }

    void ConfirmDistraction()
    {
        //Randomiza el niño que se distraerá;
        index = UnityEngine.Random.Range(0, 6);
        //Si el niño llamado no se encuentra distraído, se distraerá;
        if (probabilityForKid == 2 && !kids[index].distracted)
        {
            kids[index].distracted = true;
            DistractorsForKids();
        }
        //Si el niño llamado se encuentra distraído, entonces la funcion se repite hasta que encuentre otro niño para distraer;
        else if(probabilityForKid == 2)
        {
            ConfirmDistraction();
        }
    }
    //Asigna el niño al distractor más cercano existente y que no ha llamado a algun otro niño anteriormente.
    void DistractorsForKids()
    {
        foreach (Kid k in kids)
        {
            if (k.distracted && k.distraction == null)
            {
                k.distraction = GameObject.FindWithTag("Trap").transform;
                Distract d = k.distraction.GetComponent<Distract>();
                d.called = true;
            }
        }
        StartCoroutine(DistractingKids());
    }
}
