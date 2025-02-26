using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

public class AnimalSpawner : MonoBehaviour
{
    public WaveFunction waveFunction;
    public GameObject[] daylightAnimals;
    public GameObject[] middleAnimals;
    public GameObject[] abyssAnimals;
    private float scalingOverTime = 0.0f;
    public int animalsPerZone = 5;
    public float spawnInterval = 10f; // Set the interval to 10 seconds

    private void Update()
    {
        scalingOverTime += Time.deltaTime;
    }
    private void OnEnable()
    {
        WaveFunction.OnMapGenerationComplete += StartSpawningAnimals;
    }

    private void OnDisable()
    {
        WaveFunction.OnMapGenerationComplete -= StartSpawningAnimals;
        StopAllCoroutines();
    }

    void StartSpawningAnimals()
    {
        scalingOverTime = 0.0f;
        StartCoroutine(SpawnAnimalsRepeatedly());
    }

    IEnumerator SpawnAnimalsRepeatedly()
    {
        while (true)
        {
            SpawnAnimals();
            yield return new WaitForSeconds(spawnInterval); // Wait 10 seconds before spawning again
        }
    }

    void SpawnAnimals()
    {
        Debug.Log("Spawning animals!");

        List<Cell> shuffledGrid = new List<Cell>(waveFunction.gridComponents);
        ShuffleList(shuffledGrid);

        int daylightCount = 0, middleCount = 0, abyssCount = 0;
        List<Vector2> usedPositions = new List<Vector2>();

        foreach (Cell cell in shuffledGrid)
        {
            if (cell.collapsed && !usedPositions.Contains(cell.transform.position))
            {
                Vector2 spawnPosition = cell.transform.position;

                switch (cell.zoneType)
                {
                    case ZoneType.Daylight:
                        if (daylightCount < animalsPerZone)
                        {
                            SpawnAnimal(daylightAnimals, spawnPosition);
                            daylightCount++;
                        }
                        break;
                    case ZoneType.Middle:
                        if (middleCount < animalsPerZone)
                        {
                            SpawnAnimal(middleAnimals, spawnPosition);
                            middleCount++;
                        }
                        break;
                    case ZoneType.Abyss:
                        if (abyssCount < animalsPerZone)
                        {
                            SpawnAnimal(abyssAnimals, spawnPosition);
                            abyssCount++;
                        }
                        break;
                }

                usedPositions.Add(spawnPosition);
                if (daylightCount >= animalsPerZone && middleCount >= animalsPerZone && abyssCount >= animalsPerZone)
                    break;
            }
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    void SpawnAnimal(GameObject[] animalPrefabs, Vector2 position)
    {
        if (animalPrefabs.Length == 0) return;
        GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        Instantiate(animalPrefab, position, Quaternion.identity).tag = "Animal";
        animalPrefab.transform.localScale = Vector3.one * (1f+scalingOverTime/25); // Increases scale based on time passed (adjust as needed)
    }
}
