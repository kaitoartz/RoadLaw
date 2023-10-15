using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGenerator : MonoBehaviour
{
    public List<GameObject> carsPool;
    public GameObject carPrefab;
    public string spawnPointName = "Spawn"; // Nombre del GameObject que actúa como el punto de generación

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

            // Buscar el GameObject llamado "Spawn" en la escena
            GameObject spawnPoint = GameObject.Find(spawnPointName);

            if (spawnPoint != null)
            {
                car.transform.position = spawnPoint.transform.position;
                car.SetActive(true);
            }
            else
            {
                Debug.LogError("No se encontró el GameObject con el nombre " + spawnPointName);
            }

            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    GameObject GetInactiveCar()
    {
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        foreach (GameObject car in carsPool)
        {
            if (!car.activeInHierarchy)
            {
                return car;
            }
        }

        GameObject newCar = Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        carsPool.Add(newCar);

        return newCar;
    }
}
