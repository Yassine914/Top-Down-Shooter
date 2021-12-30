using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject healthObj;
    [SerializeField] private Sprite[] healthSprite;
    private int equippedSprite;

    private void Start()
    {
        equippedSprite = healthSprite.Length;
    }

    private void Update()
    {
        var health = GetComponent<EnemyCollision>().health;
        Debug.Log(health);
        HealthSpriteChange(health);
    }
    
    void HealthSpriteChange(int health)
    {
        equippedSprite = health;
        
            if(equippedSprite <= healthSprite.Length)
                healthObj.GetComponent<SpriteRenderer>().sprite = healthSprite[equippedSprite];

    }
}
