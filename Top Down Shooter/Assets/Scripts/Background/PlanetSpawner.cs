using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;
    private GameObject planetRing;
    private bool planetRingBool;
    private int planetSpawnRate;
    private int planetCount;
    
    private void Start()
    {
        planetCount = Random.Range(9, 13);
    }

    private void Update()
    {
        while (planetSpawnRate < planetCount)
        {
            SpawnPlanets();
            planetSpawnRate++;
        }
    }

    private void SpawnPlanets()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-9f, 9.1f),Random.Range(-5f, 5.1f), 0f);
        var randomPlanet = Random.Range(0, planets.Length);
        var planetRing = Random.Range(0, 2);
        var planetSize = Random.Range(0.5f, 1.2f);

        if (planetRing == 1)
            planetRingBool = true;
        else
            planetRingBool = false;


        GameObject planet = Instantiate(planets[randomPlanet], spawnPoint, quaternion.identity);
        planet.transform.localScale = new Vector3(planetSize, planetSize, 1);
        planet.transform.parent = transform;
        
        planet.transform.GetChild(0).gameObject.SetActive(planetRingBool);
    }
}
