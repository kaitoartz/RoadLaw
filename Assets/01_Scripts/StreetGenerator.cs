using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetGenerator : MonoBehaviour
{
    public List<GameObject> streetPrefabs;
    private Vector3 nextPosition;
    private List<GameObject> activeStreets;
    [SerializeField] private float distanceDestroy = 50f;
    [SerializeField] private float distanceCreate = 50f;

    void Start()
    {
        nextPosition = Vector3.zero;
        activeStreets = new List<GameObject>();
        GenerateStreet();
    }

    void Update()
    {
        if (nextPosition.z - transform.position.z < distanceCreate)
        {
            GenerateStreet();
        }

        // Elimina las calles que están demasiado lejos
        for (int i = activeStreets.Count - 1; i >= 0; i--)
        {
            if (activeStreets[i].transform.position.z < transform.position.z - distanceDestroy)
            {
                Destroy(activeStreets[i]);
                activeStreets.RemoveAt(i);
            }
        }
    }

    void GenerateStreet()
    {
        GameObject prefab = streetPrefabs[Random.Range(0, streetPrefabs.Count)];
        GameObject streetSection = Instantiate(prefab, nextPosition, Quaternion.identity);
        nextPosition.z += streetSection.transform.localScale.z;

        // Añade la nueva calle a la lista de calles activas
        activeStreets.Add(streetSection);
    }
}
