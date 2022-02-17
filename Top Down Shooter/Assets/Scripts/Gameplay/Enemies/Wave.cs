using UnityEngine;

[CreateAssetMenu(menuName = "Wave", fileName = "Wave #")]
public class Wave : ScriptableObject
{
    [Header("Wave")]
    public int waveNumber;
    public int minWaveEnemies;
    public int maxWaveEnemies;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    
    [Header("Enemies")]
    public int circleEnemyPercent;
    public int hexEnemyPercent;
    public int pentaEnemyPercent;
    public int bombEnemyPercent;
    public int speedEnemyPercent;
    
    [Header("Boss")]
    public bool bossLevel;
    public GameObject boss;
}
