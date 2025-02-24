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

    void Start()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        // Shuffle the grid components to randomize spawning order
        List<Cell> shuffledGrid = new List<Cell>(waveFunction.gridComponents);
        ShuffleList(shuffledGrid);

        int daylightCount = 0;
        int middleCount = 0;
        int abyssCount = 0;

        foreach (Cell cell in shuffledGrid)
        {
            Debug.Log(cell.tileOptions[0].zoneType);
            if (cell.collapsed)
            {
                Tile tile = cell.tileOptions[0];
                Vector2 spawnPosition = cell.transform.position;

                // Spawn animals only if the tile is in the correct zone and the limit is not reached
                switch (tile.zoneType)
                {
                    case ZoneType.Daylight:
                        if (daylightAnimals.Length > 0 && daylightCount < animalsPerZone)
                        {
                            Debug.Log("D");
                            SpawnAnimal(daylightAnimals, spawnPosition);
                            daylightCount++;
                        }
                        break;
                    case ZoneType.Middle:
                        if (middleAnimals.Length > 0 && middleCount < animalsPerZone)
                        {
                            Debug.Log("M");
                            SpawnAnimal(middleAnimals, spawnPosition);
                            middleCount++;
                        }
                        break;
                    case ZoneType.Abyss:
                        if (abyssAnimals.Length > 0 && abyssCount < animalsPerZone)
                        {
                            Debug.Log("A");
                            SpawnAnimal(abyssAnimals, spawnPosition);
                            abyssCount++;
                        }
                        break;
                }
            }
        }
    }

    // Fisher-Yates shuffle algorithm
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

    void SpawnAnimal(GameObject[] animalPrefabs, Vector2 position)
    {
        if (animalPrefabs.Length == 0) return;

        for (int i = 0; i < animalsPerZone; i++)
        {
            GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
            Instantiate(animalPrefab, position, Quaternion.identity);
        }
    }
}