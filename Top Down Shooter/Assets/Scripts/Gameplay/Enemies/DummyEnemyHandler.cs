using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DummyEnemyHandler : MonoBehaviour
{
    #region Variables

    [SerializeField] private Enemy enemyInfo;
    
    [Header("Health")]
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer;

    [Header("Coins")] 
    [SerializeField] private GameObject coin;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    [Header("Death")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private bool willSpawnEnemy;
    
    [HideInInspector] public string enemyName;
    private int _enemyHealth;
    [HideInInspector] public float enemyLookSpeed;
    
    private GameObject cam;
    private Animator cameraAnim;
    
    private Transform _player;
    private Vector3 _playerPos;

    #endregion
    
    private void Awake()
    {
        enemyName = enemyInfo.enemyName;
        _enemyHealth = enemyInfo.enemyHealth;
        enemyLookSpeed = enemyInfo.enemyLookSpeed;

        if (Camera.main is not null) cam = Camera.main.gameObject;

        healthRenderer.sprite = healthSprites.Last();
    }

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        cameraAnim = cam.GetComponent<Animator>();
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
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        
        var enemyPos = transform.position;

        Vector3 dir = (playerPos - enemyPos);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyLookSpeed);
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
        var exp = Instantiate(explosion, transform.position, quaternion.identity);
        Destroy(exp, 0.8f);
        cameraAnim.SetTrigger("ShakeCamEnemy");
        
        if(willSpawnEnemy)
            FindObjectOfType<ShootTutorialHandler>().SpawnSecondEnemy();
        else if (!willSpawnEnemy)
            FindObjectOfType<ShootTutorialHandler>().StartNextTut();

        if(SceneManager.GetActiveScene().name is "Wave Mode Easy" or "Wave Mode Hard")
            FindObjectOfType<EnemyWaveHandler>().enemiesKilled++;
    }
}
