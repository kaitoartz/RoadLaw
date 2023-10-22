using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Kid[] kids;
    public GameObject[] traps;
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
        if (kids[index].distraction == null)
        {
            traps = GameObject.FindGameObjectsWithTag("Trap");
            GameObject closestObject = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = kids[index].transform.position;

            foreach (GameObject trap in traps)
            {
                float distance = Vector3.Distance(trap.transform.position, currentPosition);
                if (distance < closestDistance)
                {
                    closestObject = trap;
                    closestDistance = distance;
                }
            }

            if (closestObject != null && !closestObject.GetComponent<Distract>().called)
            {
                Distract d = closestObject.GetComponent<Distract>();
                d.called = true;
                kids[index].distraction = closestObject.transform;
                StartCoroutine(DistractingKids());
            }
            else
            {
                print("Leer");
                DistractorsForKids();
            }
        }
    }
}
