using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemyInfo;
    
    [Header("Coins")] 
    [SerializeField] private GameObject coin;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;
    
    [Header("Death")]
    [SerializeField] private GameObject explosion1;
    [SerializeField] private Transform[] explosionLocations;

    [HideInInspector] public int enemyDamage;
    private Transform _player;
    private bool diedFromBullet;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDamage = enemyInfo.enemyDamage;
    }
    
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        
        FollowPlayer(_player.position);
            
        if(enemyInfo.enemyHealth <= 0)
            Destroy(gameObject); DeathExplosion();
        
        if(enemyInfo.enemyHealth <= 0 && diedFromBullet)
            SpawnCoins(); DeathExplosion();
    }
    
    private void FollowPlayer(Vector3 playerPos)
    {
        var enemyPos = transform.position;
        
        transform.position = Vector2.MoveTowards(enemyPos,
            playerPos , enemyInfo.enemySpeed * Time.deltaTime);

            Vector3 dir = (playerPos - enemyPos);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyInfo.enemyLookSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullets"))
        {
            enemyInfo.enemyHealth--;
            diedFromBullet = true;
        }

        if (other.collider.CompareTag("Player"))
        {
            enemyInfo.enemyHealth -= enemyInfo.enemyHealth;
            other.gameObject.GetComponent<PlayerHealthAndCoins>().health -= enemyDamage;
            diedFromBullet = false;
        }
    }
    
    private void SpawnCoins()
    {
        var coinSpawn = Random.Range(minCoins, maxCoins);

        var pos = transform.position;
        for (int i = 0; i < coinSpawn; i++)
        {
            Instantiate(coin, pos, quaternion.identity);
            pos += new Vector3(Random.Range(-0.4f, 0.5f), Random.Range(-0.4f, 0.4f), 0);
        }
    }
    
    private void DeathExplosion()
    {
        foreach (var t in explosionLocations)
        {
            Instantiate(explosion1, t.position, quaternion.identity);
        }
    }
}
