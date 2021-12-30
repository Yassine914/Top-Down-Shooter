using System;
using UnityEngine;

    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        [HideInInspector] public int health;

        private void Awake()
        {
            health = enemy.enemyHealth;
        }

        private void Update()
        {
            health = enemy.enemyHealth;

            if (enemy.enemyHealth <= 0)
            {
                enemy.enemyHealth = 0;
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Bullets"))
            {
                enemy.enemyHealth --;
                Debug.Log(enemy.enemyHealth);
            }
        }
    }

