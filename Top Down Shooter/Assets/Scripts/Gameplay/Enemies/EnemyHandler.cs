using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private Enemy enemyInfo;
    
    [Header("Health")]
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer;

    [Header("Follow Player")] 
    [Tooltip("This Defines The Shooting Accuracy")] 
    [SerializeField] private float enemyLookSpd;

    [Header("Shooting")] 
    [SerializeField] private Transform shootPoint1;
    [SerializeField] private Transform shootPoint2;
    [SerializeField] private GameObject bulletPrefab;
    
    private string _enemyName;
    private int _enemyHealth;
    [HideInInspector] public int enemyDamage;
    private float _enemySpeed;
    private float _enemyShootSpeed;
    private float _enemyShootDelay;
    
    private Transform _player;
    private Vector3 _playerPos;
    
    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        _enemyName = enemyInfo.enemyName;
        _enemyHealth = enemyInfo.enemyHealth;
        enemyDamage = enemyInfo.enemyDamage;
        _enemySpeed = enemyInfo.enemySpeed;
        _enemyShootSpeed = enemyInfo.enemyShootSpeed;
        _enemyShootDelay = enemyInfo.enemyShootDelay;
        
        healthRenderer.sprite = healthSprites.Last();
    }

    private void Start()
    {
        StartCoroutine(ShootDelay());
    }

    private void Update()
    {
        _playerPos = _player.transform.position;
        FollowPlayer(_playerPos);
        
        
        if(_enemyHealth <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullets"))
        {
            _enemyHealth--;

            if (_enemyHealth > 0 && _enemyHealth != 1)
            {
                healthRenderer.sprite = healthSprites[(_enemyHealth / 2) - 1];
            }
        }
    }

    private void FollowPlayer(Vector3 playerPos)
    {
        var enemyPos = transform.position;
        transform.position = Vector3.MoveTowards(enemyPos, playerPos , _enemySpeed * Time.deltaTime);
        
        Vector3 dir = (playerPos - enemyPos);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyLookSpd);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_enemyShootDelay);
        Shoot();
        StartCoroutine(ShootDelay());
    }
    
    private void Shoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, shootPoint1.position, shootPoint1.rotation);
        Rigidbody2D bulletRb1 = bullet1.GetComponent<Rigidbody2D>();
        bulletRb1.AddForce(shootPoint1.up * _enemyShootSpeed, ForceMode2D.Impulse);
        
        GameObject bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
        Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
        bulletRb2.AddForce(shootPoint2.up * _enemyShootSpeed, ForceMode2D.Impulse);
    }
}