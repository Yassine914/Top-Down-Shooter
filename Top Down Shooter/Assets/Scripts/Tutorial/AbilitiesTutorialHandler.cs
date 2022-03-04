using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitiesTutorialHandler : MonoBehaviour
{
    [Header("Ability Objects")] 
    [SerializeField] private GameObject abilitiesText;
    [SerializeField] private GameObject shieldInfo, bulletInfo, dashInfo;
    [SerializeField] private GameObject shieldButton, bulletButton, dashButton;

    [SerializeField] private GameObject nextTutButton;

    [Header("Enemies")] 
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject circleIndicator;

    private int wallNo;
    private float xValue;
    private float yValue;
    private float minXValue;
    private float maxXValue;
    private float minYValue;
    private float maxYValue;
    
    private void Awake()
    {
        if (Camera.main is null) return;
        var screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
            Screen.height, 0));

        minXValue = - screenPos.x + 1;
        maxXValue = screenPos.x - 1;
        minYValue = -screenPos.y + 1;
        maxYValue = screenPos.y - 1;
    }
    
    private void Start()
    {
        DeActivateObject(shieldInfo);
        DeActivateObject(bulletInfo);
        DeActivateObject(dashInfo);
        DeActivateObject(abilitiesText);
        
        DeActivateObject(shieldButton);
        DeActivateObject(bulletButton);
        DeActivateObject(dashButton);
        
        Invoke(nameof(StartShield), 1f);
    }
    
    private Vector3 SpawnLocation()
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

    private void DeActivateObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void ActivateObject(GameObject gameObject, float tweenTime)
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), tweenTime);
    }

    private void StartShield()
    {
        ActivateObject(abilitiesText, 0.5f);
        ActivateObject(shieldInfo, 0.6f);
        ActivateObject(shieldButton, 0.6f);
    }

    public void EndShield()
    {
        StartCoroutine(EndObject(shieldInfo, shieldButton, 0.3f));
        StartCoroutine(StartBulletDelay());
    }

    private IEnumerator StartBulletDelay()
    {
        yield return new WaitForSeconds(1f);
        StartBullet();
    }
    
    private IEnumerator EndObject(GameObject gameObject1, GameObject gameObject2, float tweenTime)
    {
        LeanTween.scale(gameObject1, new Vector3(0, 0, 0), tweenTime);
        LeanTween.scale(gameObject2, new Vector3(0, 0, 0), tweenTime);
        
        yield return new WaitForSeconds(tweenTime + 0.1f);
        
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
    }
    
    private void StartBullet()
    {
        ActivateObject(bulletInfo, 0.6f);
        ActivateObject(bulletButton, 0.6f);
    }
    
    public void EndBullet()
    {
        StartCoroutine(EndObject(bulletInfo, bulletButton, 0.3f));
        StartCoroutine(StartDashDelay());
    }
    
    private IEnumerator StartDashDelay()
    {
        yield return new WaitForSeconds(1f);
        StartDash();
    }
    
    private void StartDash()
    {
        ActivateObject(dashInfo, 0.6f);
        ActivateObject(dashButton, 0.6f);
    }
    
    public void EndDash()
    {
        StartCoroutine(EndObject(dashInfo, dashButton, 0.3f));
        StartCoroutine(AbilityTextEnd());
    }

    private IEnumerator AbilityTextEnd()
    {
        LeanTween.scale(abilitiesText, new Vector3(0, 0, 0), 0.5f);
        nextTutButton.SetActive(true);
        LeanTween.scale(nextTutButton, new Vector3(1, 1, 1), 0.5f);
        nextTutButton.transform.GetChild(0).gameObject.SetActive(false);
        nextTutButton.transform.GetChild(1).gameObject.SetActive(false);
        nextTutButton.transform.GetChild(2).gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.6f);
        
        abilitiesText.SetActive(false);
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        var loc = SpawnLocation();

        Instantiate(circleIndicator, loc, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(enemy, loc, quaternion.identity);
    }
}
