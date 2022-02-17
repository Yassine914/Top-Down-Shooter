using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    
    [Header("Misc")] 
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject circleIndicator;
    [SerializeField] private GameObject bossHpBar;
    [SerializeField] private Color waveTextNormalColor;
    [SerializeField] private Color waveTextBossColor;

    private int wallNo;
    private float xValue;
    private float yValue;
    
    private GameObject wavesTextObj;
    private GameObject countdownTextObj;

    private void Awake()
    {
        if (Camera.main is null) return;
        var screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height, 0));

        minXValue = - screenPos.x + 1;
        maxXValue = screenPos.x - 1;
        minYValue = -screenPos.y + 1;
        maxYValue = screenPos.y - 1;

        bossHpBar.SetActive(false);
    }

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
        if (waves[wave].bossLevel)
        {
            wavesText.color = waveTextBossColor;
            wavesText.text = "BOSS INCOMING!";
        }
        else
        {
            wavesText.color = waveTextNormalColor;
            wavesText.text = "Wave #" + (wave + 1);
        }
        wavesTextObj.SetActive(true);
        LeanTween.scale(wavesTextObj, new Vector3(1, 1, 1), 0.5f);
        
        yield return new WaitForSeconds(1f);
        LeanTween.scale(wavesTextObj, new Vector3(0, 0, 0), 0.5f);
        
        yield return new WaitForSeconds(0.5f);
        wavesText.gameObject.SetActive(false);
        
        countdownTextObj.SetActive(true);
        countdownText.text = "3";
        LeanTween.scale(countdownTextObj, new Vector3(1, 1, 1), 0.4f);
        
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "2";
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "1";
        yield return new WaitForSeconds(0.5f);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.6f);
        LeanTween.scale(countdownTextObj, new Vector3(0, 0, 0), 0.3f);
        StartCoroutine(Wave(wave));
        
        yield return new WaitForSeconds(0.2f);
        countdownTextObj.SetActive(false);
    }

    private IEnumerator Wave(int wave) //Wave
    {
        if (waves[wave].bossLevel)
        {
            bossHpBar.SetActive(true);
            var loc = SpawnLocation();
            
            Instantiate(circleIndicator, loc, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
            GameObject boss = Instantiate(waves[wave].boss, loc, Quaternion.identity);
            bossHpBar.GetComponent<Slider>().maxValue = boss.GetComponent<BossHandler>().health;

            var noOfTries = 100;
            
            for (int i = 0; i < noOfTries; i++)
            {
                yield return new WaitForSeconds(5f);
                if (GameObject.FindGameObjectsWithTag("Boss").Length == 0 && GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
                {
                    bossHpBar.SetActive(false);
                    StartCoroutine(WaveComplete());
                    yield return new WaitForSeconds(2f);
                    StartCoroutine(WaveCountdown(wave + 1));
                    break;
                }
            }
        }
        else
        {
            var waveEnemies = Random.Range(waves[wave].minWaveEnemies, waves[wave].maxWaveEnemies);
            var enemiesSpawned = 0;

            yield return new WaitForSeconds(1f);
        
            for (int i = 0; i < waveEnemies; i++)
            {
                if(GameObject.FindGameObjectsWithTag("Player").Length == 0) yield break;

                var loc = SpawnLocation();

                Instantiate(circleIndicator, loc, Quaternion.identity);
                yield return new WaitForSeconds(0.6f);
            
                Instantiate(enemies[RandomEnemy(wave)], loc, Quaternion.identity);
                enemiesSpawned += 1;
                yield return new WaitForSeconds(Random.Range(waves[wave].minTimeBetweenSpawns, waves[wave].maxTimeBetweenSpawns));
            }

            if (enemiesSpawned >= waveEnemies)
            {
                var noOfTries = 48;
            
                for (int i = 0; i < noOfTries; i++)
                {
                    yield return new WaitForSeconds(5f);
                    if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
                    {
                        StartCoroutine(WaveComplete());
                        yield return new WaitForSeconds(1.15f);
                        StartCoroutine(WaveCountdown(wave + 1));
                        break;
                    }
                }
            }
        }
    }

    private IEnumerator WaveComplete()
    {
        wavesText.color = waveTextNormalColor;
        wavesText.text = "Wave Completed";
        wavesTextObj.SetActive(true);
        LeanTween.scale(wavesTextObj, new Vector3(1, 1, 1), 0.3f);
        
        yield return new WaitForSeconds(0.8f);
        LeanTween.scale(wavesTextObj, new Vector3(0, 0, 0), 0.3f);
        
        yield return new WaitForSeconds(0.35f);
        wavesText.gameObject.SetActive(false);
    }
}