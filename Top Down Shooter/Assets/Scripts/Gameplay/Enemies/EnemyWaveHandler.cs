using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveHandler : MonoBehaviour
{ 
    [SerializeField] private Wave[] waves;

    [SerializeField] private GameObject circleEnemy;
    [SerializeField] private GameObject hexEnemy;
    [SerializeField] private GameObject pentaEnemy;
    [SerializeField] private GameObject bombEnemy;

    [Header("SpawnArea")]
    [SerializeField] private float minXValue;
    [SerializeField] private float maxXValue;
    [SerializeField] private float minYValue;
    [SerializeField] private float maxYValue;
    private int wallNo;
    private float xValue;
    private float yValue;


    private void Start()
    {
        Instantiate(pentaEnemy, SpawnLocation(), quaternion.identity);
        Instantiate(hexEnemy, SpawnLocation(), quaternion.identity);
        Instantiate(pentaEnemy, SpawnLocation(), quaternion.identity);
        Instantiate(bombEnemy, SpawnLocation(), quaternion.identity);
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
}