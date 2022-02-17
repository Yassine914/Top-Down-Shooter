using System.Collections;
using UnityEngine;

public class BossChooser : MonoBehaviour
{
    [SerializeField] private GameObject[] bossesEasy;
    [SerializeField] private GameObject[] bossesMedium;
    [SerializeField] private GameObject[] bossesHard;
    
    [SerializeField] private GameObject circleIndicator;

    [SerializeField] private int easyPercent;
    [SerializeField] private int mediumPercent;
    [SerializeField] private int hardPercent;

    private Vector2 screenPos;
    private Vector2 randomLoc;
    
    private void Start()
    {
        if (Camera.main is not null)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height, 0));
        }
        
        GetRandomLocation();
        StartCoroutine(SpawnBossIndicator());
    }

    private int GetRandomBossType()
    {
        var randomValue = Random.Range(0, 101);

        if (0 <= randomValue && randomValue < hardPercent)
        {
            return 2;
        }

        if (hardPercent < randomValue && randomValue <= (hardPercent + mediumPercent))
        {
            return 1;
        }

        if ((mediumPercent + hardPercent) < randomValue)
        {
            return 0;
        }
        
        return 0;
    }
    
    private int GetRandomBossNumber()
    {
        var random = Random.Range(0, 3);
        return random;
    }

    private void GetRandomLocation()
    {
        randomLoc = new Vector2(Random.Range(-screenPos.x + 1, screenPos.x - 1), Random.Range(-screenPos.y + 1, screenPos.y - 1));
    }
    
    private IEnumerator SpawnBossIndicator()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(circleIndicator, randomLoc, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        SpawnBoss();
    }
    
    private void SpawnBoss()
    {
        var bossType = GetRandomBossType();
        var bossNumber = GetRandomBossNumber();
        
        switch (bossType)
        {
            case 0:
                Instantiate(bossesEasy[bossNumber], randomLoc, Quaternion.identity);
                break;
            case 1:
                Instantiate(bossesMedium[bossNumber], randomLoc, Quaternion.identity);
                break;
            case 2:
                Instantiate(bossesHard[bossNumber], randomLoc, Quaternion.identity);
                break;
        }
    }
}
