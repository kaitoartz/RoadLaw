using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGenerator : MonoBehaviour
{
    public List<GameObject> carsPool;
    public GameObject carPrefab;
    public GameObject spawnPoint;

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
            car.transform.position = spawnPoint.transform.position;
            car.SetActive(true);

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

        GameObject newCar = Instantiate(carPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        carsPool.Add(newCar);

        return newCar;
    }
}
