using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGenerator : MonoBehaviour
{
    public List<GameObject> carsPool;
    public GameObject carPrefab;
    public List<string> spawnPointNames; // Lista de nombres de GameObjects que actúan como puntos de generación

    public float minSpawnTime;
    public float maxSpawnTime;
    private void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            GameObject car = GetInactiveCar();

            // Filtra la lista de nombres para obtener solo los spawners activos
            List<string> activeSpawnPointNames = spawnPointNames.FindAll(name => GameObject.Find(name) != null && GameObject.Find(name).activeInHierarchy);

            // Si hay spawners activos, selecciona aleatoriamente uno de ellos
            if (activeSpawnPointNames.Count > 0)
            {
                string randomSpawnPointName = activeSpawnPointNames[Random.Range(0, activeSpawnPointNames.Count)];

                GameObject spawnPoint = GameObject.Find(randomSpawnPointName);

                if (spawnPoint != null)
                {
                    car.transform.rotation = spawnPoint.transform.rotation;
                    car.transform.position = spawnPoint.transform.position;
                    car.SetActive(true);
                }
                else
                {
                    Debug.LogError("No se encontró el GameObject con el nombre " + randomSpawnPointName);
                }
            }
            else
            {
                Debug.LogWarning("No hay spawners activos disponibles.");
            }

            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    GameObject GetInactiveCar()
    {
        foreach (GameObject car in carsPool)
        {
            if (!car.activeInHierarchy)
            {
                return car;
            }
        }

        GameObject newCar = Instantiate(carPrefab);
        carsPool.Add(newCar);
        return newCar;
    }
}
