using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveHandler : MonoBehaviour
{
    
    [Header("Waves")] 
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject[] enemies;

    [Header("SpawnArea")]
    [SerializeField] private float minXValue;
    [SerializeField] private float maxXValue;
    [SerializeField] private float minYValue;
    [SerializeField] private float maxYValue;
    private int wallNo;
    private float xValue;
    private float yValue;

    [Header("Misc")] 
    [SerializeField] private TextMeshProUGUI wavesText;
    private GameObject wavesTextObj;
    [SerializeField] private TextMeshProUGUI countdownText;
    private GameObject countdownTextObj;

    private void Start() //Start With First Wave
    {
        wavesTextObj = wavesText.gameObject;
        countdownTextObj = countdownText.gameObject;
        
        countdownTextObj.transform.localScale = new Vector3(0, 0, 0);
        wavesTextObj.transform.localScale = new Vector3(0, 0, 0);
        
        wavesTextObj.SetActive(false);
        countdownTextObj.SetActive(false);

        StartCoroutine(WaveCountdown(0));
    }

    private Vector3 SpawnLocation() //Returns Random Spawn Point
    {
        wallNo = Random.Range(1, 5);

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
            case 3:
                yValue = minYValue;
                xValue = Random.Range(minXValue, maxXValue);
                break;
            case 4:
                xValue = minXValue;
                yValue = Random.Range(minYValue, maxYValue);
                break;
        }

        Vector3 location = new Vector3(xValue, yValue, 0);
        
        return location;
    }

    private int RandomEnemy(int wave) //Returns Random Enemy
    {
        var hexPercent = waves[wave].hexEnemyPercent;
        var pentaPercent = waves[wave].pentaEnemyPercent;
        var bombPercent = waves[wave].bombEnemyPercent;
        var speedPercent = waves[wave].speedEnemyPercent;

        int randomValue = Random.Range(0, 101);

        if (0 <= randomValue && randomValue < speedPercent)
        {
            // Speed Enemy
            return 4;
        }

        if (speedPercent < randomValue && randomValue <= (speedPercent + bombPercent))
        {
            // Bomb Enemy
            return 3;
        }

        if ((speedPercent + bombPercent) < randomValue &&
            randomValue <= (speedPercent + bombPercent + pentaPercent))
        {
            // Penta Enemy
            return 2;
        }

        if ((speedPercent + bombPercent + pentaPercent) < randomValue &&
            randomValue <= (speedPercent + bombPercent + pentaPercent + hexPercent))
        {
            // Hex Enemy
            return 1;
        }

        if ((speedPercent + bombPercent + pentaPercent + hexPercent) < randomValue)
        {
            // Circle Enemy
            return 0;
        }

        return 0;
    }
    
    private IEnumerator WaveCountdown(int wave) //Wave Countdown Starts and then the Wave Starts
    {
        wavesText.text = "Wave #" + (wave + 1);
        wavesTextObj.SetActive(true);
        LeanTween.scale(wavesTextObj, new Vector3(1, 1, 1), 0.5f);
        
        yield return new WaitForSeconds(1.6f);
        LeanTween.scale(wavesTextObj, new Vector3(0, 0, 0), 0.5f);
        
        yield return new WaitForSeconds(0.5f);
        wavesText.gameObject.SetActive(false);
        
        countdownTextObj.SetActive(true);
        countdownText.text = "3";
        LeanTween.scale(countdownTextObj, new Vector3(1, 1, 1), 0.4f);
        
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.7f);
        LeanTween.scale(countdownTextObj, new Vector3(0, 0, 0), 0.4f);
        yield return new WaitForSeconds(0.4f);
        countdownTextObj.SetActive(false);

        StartCoroutine(Wave(wave));
    }

    private IEnumerator Wave(int wave) //Wave
    {
        var waveEnemies = Random.Range(waves[wave].minWaveEnemies, waves[wave].maxWaveEnemies);
        var enemiesSpawned = 0;
        Debug.Log( "Wave #" + (wave + 1) + ", Wave Enemies: " + waveEnemies);

        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < waveEnemies; i++)
        {
            if(GameObject.FindGameObjectsWithTag("Player").Length == 0) yield break;
            
            Instantiate(enemies[RandomEnemy(wave)], SpawnLocation(), Quaternion.identity);
            enemiesSpawned += 1;
            Debug.Log("Enemies Spawned: " + enemiesSpawned);
            yield return new WaitForSeconds(Random.Range(waves[wave].minTimeBetweenSpawns, waves[wave].maxTimeBetweenSpawns));
        }

        if (enemiesSpawned >= waveEnemies)
        {
            var noOfTries = 50;
            
            for (int i = 0; i < noOfTries; i++)
            {
                yield return new WaitForSeconds(5f);
                if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
                {
                    yield return new WaitForSeconds(4f);
                    StartCoroutine(WaveCountdown(wave + 1));
                    break;
                }
            }
        }
    }
}