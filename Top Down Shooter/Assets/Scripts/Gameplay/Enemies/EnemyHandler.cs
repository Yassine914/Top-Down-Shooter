using System.Collections;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHandler : MonoBehaviour
{
    #region Variables

    [SerializeField] private Enemy enemyInfo;
    
    [Header("Health")]
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer;

    [Header("Shooting")] 
    [SerializeField] private Transform shootPoint1;
    [SerializeField] private Transform shootPoint2;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Coins")] 
    [SerializeField] private GameObject coin;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    [Header("Death")]
    [SerializeField] private GameObject explosion1;
    [SerializeField] private Transform[] explosionLocations;

    [HideInInspector] public string enemyName;
    private int _enemyHealth;
    [HideInInspector] public int enemyDamage;
    [HideInInspector] public float enemySpeed;
    [HideInInspector] public float enemyShootSpeed;
    [HideInInspector] public float enemyShootDelay;
    [HideInInspector] public float minDistFromPlayer;
    [HideInInspector] public float enemyLookSpeed;
    
    private Transform _player;
    private Vector3 _playerPos;

    #endregion
    
    private void Awake()
    {
        enemyName = enemyInfo.enemyName;
        _enemyHealth = enemyInfo.enemyHealth;
        enemyDamage = enemyInfo.enemyDamage;
        enemySpeed = enemyInfo.enemySpeed;
        enemyShootSpeed = enemyInfo.enemyShootSpeed;
        enemyShootDelay = enemyInfo.enemyShootDelay;
        minDistFromPlayer = enemyInfo.minDistFromPlayer;
        enemyLookSpeed = enemyInfo.enemyLookSpeed;
        
        healthRenderer.sprite = healthSprites.Last();
    }

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        
        StartCoroutine(ShootDelay());
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;

        _playerPos = _player.position;
        FollowPlayer(_playerPos);

        if (_enemyHealth <= 0)
        {
            SpawnCoins();
            DeathExplosion();
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //Damage Calculation
    {
        if (other.collider.CompareTag("PlayerBullets") && enemyName != "Penta")
        {
            _enemyHealth--;

            if (_enemyHealth > 0 && _enemyHealth != 1)
                healthRenderer.sprite = healthSprites[(_enemyHealth / 2) - 1];
        }

        if (other.collider.CompareTag("PlayerBullets") && enemyName == "Penta")
        {
            if (GetComponent<PentaEnemyAbility>().shieldIsActive == false)
            {
                _enemyHealth--;

                if (_enemyHealth > 0 && _enemyHealth != 1)
                    healthRenderer.sprite = healthSprites[(_enemyHealth / 2) - 1];
            }
        }
    }

    private void FollowPlayer(Vector3 playerPos) //Follow Player & Look At Him
    {
        var enemyPos = transform.position;
        if (Vector2.Distance(playerPos, enemyPos) > minDistFromPlayer)
        {
            transform.position = Vector2.MoveTowards(enemyPos, playerPos , enemySpeed * Time.deltaTime);
        }

        Vector3 dir = (playerPos - enemyPos);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyLookSpeed);
    }

    private IEnumerator ShootDelay() //Time Between Bullets
    {
        yield return new WaitForSeconds(enemyShootDelay);

        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) yield break;
        Shoot();
        StartCoroutine(ShootDelay());
    }
    
    private void Shoot() //Shoot
    {
        GameObject bullet1 = Instantiate(bulletPrefab, shootPoint1.position, shootPoint1.rotation);
        Rigidbody2D bulletRb1 = bullet1.GetComponent<Rigidbody2D>();
        bulletRb1.AddForce(shootPoint1.up * enemyShootSpeed, ForceMode2D.Impulse);
        bullet1.GetComponent<Bullet>().bulletDmg = enemyDamage;
        
        GameObject bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
        Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
        bulletRb2.AddForce(shootPoint2.up * enemyShootSpeed, ForceMode2D.Impulse);
        bullet2.GetComponent<Bullet>().bulletDmg = enemyDamage;
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