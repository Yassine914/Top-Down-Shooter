using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] planets;
    private GameObject planetRing;

    private void Start()
    {
        SpawnPlanets();
    }

    private void SpawnPlanets()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-9, 10),Random.Range(-5, 6), 0);
        var randomPlanet = Random.Range(0, planets.Length);
        var planetRing = Random.Range(0, 1);

        GameObject planet = Instantiate(planets[randomPlanet], spawnPoint, quaternion.identity);

        Color planetRingColor = planet.GetComponentInChildren<SpriteRenderer>().color;
        
        if (planetRing == 1)
        {
            planetRingObject = planet.transform.GetChild(0);
        }
        else
        {
            planet.GetComponentInChildren<SpriteRenderer>().color = planetRingColor;
        }
    }
}
