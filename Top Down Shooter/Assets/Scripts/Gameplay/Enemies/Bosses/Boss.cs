using UnityEngine;

[System.Serializable]
public class Boss
{ 
    public enum BossType { Easy, Medium, Hard }
    
    [Header("Boss Info")]
    public BossType bossType;
    public int bossNumber;
    public int numOfPhases;
    
    public Sprite[] bodySprites;
    public Sprite[] gunSprites;
    
    [Header("Health")]
    public int health;

    [Header("Moving & Looking")]
    public float moveSpeed;
    public float runSpeed;
    public float lookSpeed;
    
    [Header("Shooting")]
    public float shootDelay;
    public float shootSpeed;
    public int shootingDmg;
    
    [Header("Earthquake")]
    public int earthquakeDmg;
}
