using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Kid[] kids;
    public GameObject[] traps;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DistractingKids()
    {
        int index = UnityEngine.Random.Range(0, 6);
        int probabilityForKid = UnityEngine.Random.Range(0, 4);
        GameObject closestObject = null;
        yield return new WaitForSeconds(2f);
        if (probabilityForKid == 3)
        {
            traps = GameObject.FindGameObjectsWithTag("Trap");
            float closestDistance = Mathf.Infinity;
            Vector3 position = kids[index].transform.position;
            foreach (GameObject trap in traps)
            {
                Vector3 direction = trap.transform.position - position;
                float distance = direction.sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestObject = trap;
                    closestDistance = distance;
                }
            }

            if (closestObject != null)
            {
                kids[index].distraction = closestObject.transform;
            }
            kids[index].distracted = true;
        }
        else
        {
            
        }
        StartCoroutine(DistractingKids());
    }
}
