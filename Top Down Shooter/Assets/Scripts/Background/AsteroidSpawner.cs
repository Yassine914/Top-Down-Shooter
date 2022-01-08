using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private float minXValue;
    [SerializeField] private float maxXValue;
    [SerializeField] private float minYValue;
    [SerializeField] private float maxYValue;

    private float xValue;
    private float yValue;
    private int wallNo;
    private void Start()
    {
        StartCoroutine(AsteroidSpawnDelay());
    }

    private Vector3 AsteroidPosition()
    {
        wallNo = Random.Range(1, 3);

        switch (wallNo)
        {
            case 1:
                yValue = maxYValue;
                xValue = Random.Range(minXValue, maxXValue);
                break;
            case 2:
                xValue = maxXValue;
                yValue = Random.Range(minYValue, maxYValue);
                break;
        }
        
        Vector3 location = new Vector3(xValue, yValue, 0);
        
        return location;
    }
    
    private IEnumerator AsteroidSpawnDelay()
    {
        var delayTime = Random.Range(4f, 5f);
        var asteroidSizeRand = Random.Range(0.5f, 1.4f);
        var asteroidSize = new Vector3(asteroidSizeRand, asteroidSizeRand, asteroidSizeRand);
        
        GameObject asteroidObj = Instantiate(asteroidPrefab, AsteroidPosition(), transform.rotation);
        asteroidObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-6, -6 ), ForceMode2D.Impulse);
        asteroidObj.GetComponent<Transform>().localScale = asteroidSize;

        asteroidObj.transform.parent = transform;
        
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(AsteroidSpawnDelay());
    }
}
