using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] public int health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private Rigidbody2D rb2d;
    
    private void Awake()
    {
        healthSlider.maxValue = health;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        healthSlider.value = health;
        healthText.text = health.ToString();
        
        if (health <= 0)
            Destroy(gameObject);
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
}
