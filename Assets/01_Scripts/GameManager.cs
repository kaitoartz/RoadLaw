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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DistractingKids());
    }
    IEnumerator DistractingKids()
    {
        probabilityForKid = UnityEngine.Random.Range(0, 3);
        yield return new WaitForSeconds(2f);
        ConfirmDistraction();
    }

    void ConfirmDistraction()
    {
        index = UnityEngine.Random.Range(0, 6);
        if (probabilityForKid == 2 && !kids[index].distracted)
        {
            kids[index].distracted = true;
            DistractorsForKids();
        }
        else if(probabilityForKid == 2)
        {
            print("Leer");
            ConfirmDistraction();
        }
    }
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
    }
}
