using UnityEngine;

[CreateAssetMenu(menuName = "Wave", fileName = "Wave #")]
public class Wave : ScriptableObject
{
    [Header("Wave")]
    public int waveNumber;
    public int rounds;
    public int minRoundTime;
    public int maxRoundTime;
    public int minRoundEnemies;
    public int maxRoundEnemies;
    
    [Header("Enemies")]
    public int circleEnemyPercent;
    public int hexEnemyPercent;
    public int pentaEnemyPercent;
    public int bombEnemyPercent;
    
    [Header("Boss")]
    public bool bossLevel;
}
