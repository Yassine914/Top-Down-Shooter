using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;

    private void Start()
    {
        StartCoroutine(AsteroidSpawnDelay());
    }

    private IEnumerator AsteroidSpawnDelay()
    {
        var delayTime = Random.Range(4f, 8f);
        var spawnLocation = new Vector3(Random.Range(5f, 16f), Random.Range(-3f, 6f), 0);
        var asteroidSizeRand = Random.Range(0.7f, 1.4f);
        var asteroidSize = new Vector3(asteroidSizeRand, asteroidSizeRand, asteroidSizeRand);
        
        GameObject asteroidObj = Instantiate(asteroidPrefab, spawnLocation, transform.rotation);
        asteroidObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-6, -6 ), ForceMode2D.Impulse);
        asteroidObj.GetComponent<Transform>().localScale = asteroidSize;

        asteroidObj.transform.parent = transform;
        
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(AsteroidSpawnDelay());
    }
}
