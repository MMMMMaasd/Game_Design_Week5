using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AnimalSpawner : MonoBehaviour
{
    public WaveFunction waveFunction;
    public GameObject[] daylightAnimals;
    public GameObject[] middleAnimals;
    public GameObject[] abyssAnimals;

    public int animalsPerZone = 5;

    void OnEnable()
    {
        WaveFunction.OnMapGenerationComplete += SpawnAnimals;
    }

    void OnDisable()
    {
        WaveFunction.OnMapGenerationComplete -= SpawnAnimals;
    }

    void SpawnAnimals()
    {
        Debug.Log("Spawning animals!");

        // Shuffle the grid components to randomize spawning order
        List<Cell> shuffledGrid = new List<Cell>(waveFunction.gridComponents);
        ShuffleList(shuffledGrid);

        int daylightCount = 0;
        int middleCount = 0;
        int abyssCount = 0;

        // Store positions that have already been used for spawning to prevent duplicates
        List<Vector2> usedPositions = new List<Vector2>();

        foreach (Cell cell in shuffledGrid)
        {
            if (cell.collapsed) // Ensure we only spawn animals in collapsed cells
            {
                // Skip if the animal limit for the zone is reached or the position has already been used
                if (usedPositions.Contains(cell.transform.position)) continue;

                Vector2 spawnPosition = cell.transform.position;

                // Spawn animals only if the cell is in the correct zone and the limit is not reached
                switch (cell.zoneType)
                {
                    case ZoneType.Daylight:
                        if (daylightCount < animalsPerZone)
                        {
                            Debug.Log($"Spawning Daylight animal at ({spawnPosition.x}, {spawnPosition.y})");
                            SpawnAnimal(daylightAnimals, spawnPosition);
                            daylightCount++;
                            usedPositions.Add(spawnPosition);
                        }
                        break;

                    case ZoneType.Middle:
                        if (middleCount < animalsPerZone)
                        {
                            Debug.Log($"Spawning Middle animal at ({spawnPosition.x}, {spawnPosition.y})");
                            SpawnAnimal(middleAnimals, spawnPosition);
                            middleCount++;
                            usedPositions.Add(spawnPosition);
                        }
                        break;

                    case ZoneType.Abyss:
                        if (abyssCount < animalsPerZone)
                        {
                            Debug.Log($"Spawning Abyss animal at ({spawnPosition.x}, {spawnPosition.y})");
                            SpawnAnimal(abyssAnimals, spawnPosition);
                            abyssCount++;
                            usedPositions.Add(spawnPosition);
                        }
                        break;
                }

                // Stop spawning once the animal limit per zone is reached
                if (daylightCount >= animalsPerZone && middleCount >= animalsPerZone && abyssCount >= animalsPerZone)
                    break;
            }
        }
    }

    // Fisher-Yates shuffle algorithm to randomize the list
    void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Instantiate a single animal in the specified position
    void SpawnAnimal(GameObject[] animalPrefabs, Vector2 position)
    {
        if (animalPrefabs.Length == 0) return;

        // Spawn a single animal at the given position
        GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        GameObject animal = Instantiate(animalPrefab, position, Quaternion.identity);
        animal.tag = "Animal";
    }
}