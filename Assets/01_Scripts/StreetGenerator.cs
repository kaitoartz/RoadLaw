using System.Collections.Generic;
using UnityEngine;

public class StreetGenerator
    : MonoBehaviour
{
    public List<GameObject> streetPrefabs;
    public int poolSize = 10;

    private Vector3 nextPosition;
    private List<GameObject> activeStreets;
    private Queue<GameObject> inactiveStreets;

    public float distanceCreate = 35f;
    public float distanceDestroy = 35f;

    public GameObject guideGameObject;

    void Start()
    {
        nextPosition = Vector3.zero;
        activeStreets = new List<GameObject>();
        inactiveStreets = new Queue<GameObject>();

        // Crear un pool inicial de objetos de la calle.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject streetSection = InstantiateStreetPrefab();
            inactiveStreets.Enqueue(streetSection);
        }

        // Generar las primeras secciones de la calle al iniciar el juego.
        GenerateStreet();
    }

    void Update()
    {
        // Generar una nueva sección de la calle si el jugador avanza lo suficiente.
        if (nextPosition.z - guideGameObject.transform.position.z < distanceCreate)
        {
            GenerateStreet();
        }

        // Desactivar y reciclar las secciones de la calle que están demasiado lejos.
        for (int i = activeStreets.Count - 1; i >= 0; i--)
        {
            if (activeStreets[i].transform.position.z < guideGameObject.transform.position.z - distanceDestroy)
            {
                DeactivateStreet(activeStreets[i]);
                activeStreets.RemoveAt(i);
            }
        }
    }

    GameObject InstantiateStreetPrefab()
    {
        // Instanciar una sección de la calle de un prefab aleatorio.
        GameObject prefab = streetPrefabs[Random.Range(0, streetPrefabs.Count)];
        GameObject streetSection = Instantiate(prefab, transform);
        streetSection.SetActive(false);
        return streetSection;
    }

    void GenerateStreet()
    {
        GameObject streetSection = GetInactiveStreetSection();

        // Activar y posicionar la sección de la calle en la siguiente posición.
        streetSection.SetActive(true);
        streetSection.transform.position = nextPosition;
        nextPosition.z += streetSection.transform.localScale.z;

        // Añadir la nueva sección de la calle a la lista de secciones activas.
        activeStreets.Add(streetSection);
    }

    void DeactivateStreet(GameObject street)
    {
        // Desactivar la sección de la calle y ponerla de nuevo en el pool de objetos inactivos.
        street.SetActive(false);
        inactiveStreets.Enqueue(street);
    }

    GameObject GetInactiveStreetSection()
    {
        // Utilizar una sección de la calle inactiva del pool si está disponible.
        if (inactiveStreets.Count > 0)
        {
            return inactiveStreets.Dequeue();
        }

        // Si no hay secciones de la calle inactivas, instanciar una nueva.
        return InstantiateStreetPrefab();
    }
}
