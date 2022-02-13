using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndCoins : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] public int health;
    
    private Slider healthSlider;
    private TextMeshProUGUI healthText;

    private int coins;
    
    private void Awake()
    {
        healthSlider = GameObject.FindGameObjectWithTag("HealthBarSlider").GetComponent<Slider>();
        healthText = healthSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        healthSlider.maxValue = health;
    }

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void Update()
    {
        healthSlider.value = health;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            health = 0;
            healthText.text = health.ToString();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("EnemyBullets"))
        {
            int damage = other.gameObject.GetComponent<Bullet>().bulletDmg;
            health -= damage;
        }

        if (other.collider.CompareTag("Enemies"))
        {
            health--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            PlayerPrefs.SetInt("Coins", coins);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Earthquake"))
        {
            health -= other.GetComponentInParent<BossHandler>().earthquakeDmg;
        }
    }
    
}
