using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossHandler : MonoBehaviour
{
    #region Variables

    [Header("Basic Info")]
    [SerializeField] private Boss boss;
    [SerializeField] private SpriteRenderer bodySprite, gunSprite;
    
    [Header("Enemies Spawn")]
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float startSpawnWaitTime;
    [SerializeField] private GameObject circleIndicator;

    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint1, shootPoint2;
    [SerializeField] private GameObject gun;
    [SerializeField] private float gunPosition;
    [SerializeField] private float gunScale;

    [Header("Movement")] 
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float minDistFromPlayer;
    
    [Header("Earthquake")] 
    [SerializeField] private GameObject earthquakeObj;
    [SerializeField] private float earthquakeRadius;
    [SerializeField] private float earthquakeInterval;
    [SerializeField] private GameObject earthquakeIndicator;

    [Header("Shield")] 
    [SerializeField] private GameObject shieldObj;
    [SerializeField] private float shieldTime;
    [SerializeField] private float shieldScale;

    [Header("Explosions")] 
    [SerializeField] private GameObject explosion;

    [Header("Coins")] 
    [SerializeField] private GameObject coinObj;
    [SerializeField] private int coins;

    [HideInInspector] public float moveSpeed;
    [HideInInspector] public int earthquakeDmg;
    [HideInInspector] public float runSpeed;
    [HideInInspector] public int health;
    
    private GameObject player;
    private Slider hpBarSlider;
    private TextMeshProUGUI healthText;
    private Vector3 screenPos;
    private Animator anim;
    private Vector2 randomLocation;
    private GameObject _camera;
    private Animator cameraAnim;
    private int healthPercent;

    #endregion

    private void Awake()
    {
        gun.transform.localScale = new Vector3(0, 0, 0);
        gun.transform.position = new Vector3(0, 0, 0);
        
        hpBarSlider = GameObject.FindGameObjectWithTag("BossHpBar").GetComponent<Slider>();
        healthText = hpBarSlider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        health = boss.health;
        hpBarSlider.maxValue = health;
        hpBarSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = boss.bossType.ToString();

        if (Camera.main is not null) _camera = Camera.main.gameObject;

        moveSpeed = boss.moveSpeed;
        earthquakeDmg = boss.earthquakeDmg;
        runSpeed = boss.runSpeed;

        healthText.text = health.ToString();
    }

    private void Start()
    {
        var color = Random.Range(0, 17);
        bodySprite.sprite = boss.bodySprites[color];
        gunSprite.sprite = boss.gunSprites[color];

        anim = GetComponent<Animator>();

        shieldObj.transform.localScale = new Vector3(0, 0, 0);
        shieldObj.SetActive(false);

        earthquakeObj.transform.localScale = new Vector3(0, 0, 0);
        earthquakeObj.SetActive(false);

        cameraAnim = _camera.GetComponent<Animator>();
        
        if (Camera.main is not null)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
                Screen.height, 0));
        }
        
        healthPercent = 100 / boss.health;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdateUI();
        CheckDeath();
        LookAtPlayer();
    }

    private void UpdateUI()
    {
        hpBarSlider.value = health;
        healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullets"))
        {
            health--;
            ChangePhase();
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            healthText.text = "0";
            gameObject.SetActive(false);
            SpawnExplosions();
            SpawnCoins();
            
            Destroy(gameObject, 0.5f);
        }
    }

    private void ChangePhase()
    {
        var healthPercentage = health * ((float) 100 / boss.health);
        
        if (healthPercentage is <= 70f and > 40f)
        {
            anim.SetTrigger("GoToPhase2");
        }
        else if (healthPercentage <= 40f)
        {
            anim.SetTrigger("GoToPhase3");
        }
    }

    private void LookAtPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * boss.lookSpeed);
    }

    #region Movement

    public void StartMoveAround()
    {
        StartCoroutine(GetPos());
    }

    public void StopMoveAround()
    {
        StopAllCoroutines();
    }
    
    public void MoveAround()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomLocation, moveSpeed * Time.deltaTime);
    }
    
    private IEnumerator GetPos()
    {
        randomLocation = new Vector2(Random.Range(-screenPos.x + 2, screenPos.x - 2), Random.Range(-screenPos.y + 2, screenPos.y - 2));
        var randTime = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(randTime);
        
        StartCoroutine(GetPos());
    }

    public void MoveToPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0) return;
        
        if (Vector2.Distance(transform.position, player.transform.position) >= minDistFromPlayer)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    
    #endregion

    #region Shield
    public void StartShield()
    {
        StartCoroutine(Shield());
    }

    private IEnumerator Shield()
    {
        LeanTween.scale(shieldObj, new Vector3(shieldScale, shieldScale, shieldScale), 0.4f);
        shieldObj.SetActive(true);
        
        yield return new WaitForSeconds(shieldTime);
        
        LeanTween.scale(shieldObj, new Vector3(0, 0, 0), 0.3f);
        yield return new WaitForSeconds(0.3f);
        shieldObj.SetActive(false);
    }

    #endregion
    
    #region Earthquakes
    public void StartEarthquakes()
    {
        StartCoroutine(EarthquakeDelay());
    }

    public void StopEarthquakes()
    {
        StopAllCoroutines();
        
        SpriteRenderer r = earthquakeIndicator.GetComponent<SpriteRenderer>();
        r.color = new Color(255, 255, 255, 0);
        
        earthquakeObj.SetActive(false);
    }

    private void IndicatorOn()
    {
        LeanTween.value(earthquakeIndicator, 0f, 0.3f, 0.2f).setOnUpdate((float val) =>
        {
            SpriteRenderer r = earthquakeIndicator.GetComponent<SpriteRenderer>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
    }

    private void IndicatorOff()
    {
        LeanTween.value(earthquakeIndicator, 0.3f, 0f, 0.2f).setOnUpdate((float val) =>
        {
            SpriteRenderer r = earthquakeIndicator.GetComponent<SpriteRenderer>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
    }

    private IEnumerator EarthquakeIndicator()
    {
        IndicatorOn();
        yield return new WaitForSeconds(0.2f);
        
        IndicatorOff();
        yield return new WaitForSeconds(0.2f);
        
        IndicatorOn();
        yield return new WaitForSeconds(0.2f);
        
        IndicatorOff();
    }
    
    private IEnumerator EarthquakeDelay()
    {
        StartCoroutine(EarthquakeIndicator());
        yield return new WaitForSeconds(0.70f);
        
        earthquakeObj.SetActive(true);
        cameraAnim.SetTrigger("ShakeCamBoss");
        LeanTween.scale(earthquakeObj, new Vector3(earthquakeRadius, earthquakeRadius, earthquakeRadius), 0.35f);
        
        yield return new WaitForSeconds(0.4f);
        
        LeanTween.value(earthquakeObj, 1f, 0f, 0.2f).setOnUpdate((float val) =>
        {
            SpriteRenderer spriteRenderer = earthquakeObj.GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = val;
            spriteRenderer.color = color;
        });
        
        yield return new WaitForSeconds(0.2f);
        earthquakeObj.transform.localScale = new Vector3(0, 0, 0);
        earthquakeObj.SetActive(false);

        SpriteRenderer r = earthquakeObj.GetComponent<SpriteRenderer>();
        Color c = r.color;
        c.a = 1f;
        r.color = c;

        yield return new WaitForSeconds(earthquakeInterval);
        StartCoroutine(EarthquakeDelay());
    }

    #endregion
    
    #region SpawnEnemies

        public void StartSpawning()
        {
            StartCoroutine(SpawnStartDelay());
        }
        
        public void StopSpawning()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator SpawnDelay()
        {
            SpawnEnemies();
            yield return new WaitForSeconds(spawnInterval);
            StartCoroutine(SpawnDelay());
        }

        private IEnumerator SpawnStartDelay()
        {
            yield return new WaitForSeconds(startSpawnWaitTime);
            StartCoroutine(SpawnDelay());
        }
        
        private void SpawnEnemies()
        {
            var playerLoc = player.transform.position;
            var spawnLoc = new Vector2(Random.Range(-screenPos.x + 1, screenPos.x - 1), Random.Range(-screenPos.y + 1, screenPos.y - 1));

            if (Vector2.Distance(playerLoc, spawnLoc) >= 1)
            {
                StartCoroutine(SpawnIndicator(spawnLoc));
            }
            else
            {
                SpawnEnemies();
            }
        }

        private IEnumerator SpawnIndicator(Vector2 spawnLoc)
        {
            Instantiate(circleIndicator, spawnLoc, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
            Instantiate(enemyObj, spawnLoc, quaternion.identity);
        }
        
    #endregion
    
    #region Shooting
    
        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(boss.shootDelay);
    
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0) yield break;
            Shoot();
            StartCoroutine(ShootDelay());
        }
        
        private void Shoot()
        {
            GameObject bullet1 = Instantiate(bulletPrefab, shootPoint1.position, shootPoint1.rotation);
            Rigidbody2D bulletRb1 = bullet1.GetComponent<Rigidbody2D>();
            bulletRb1.AddForce(shootPoint1.up * boss.shootSpeed, ForceMode2D.Impulse);
            bullet1.GetComponent<Bullet>().bulletDmg = boss.shootingDmg;
            
            GameObject bullet2 = Instantiate(bulletPrefab, shootPoint2.position, shootPoint2.rotation);
            Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRb2.AddForce(shootPoint2.up * boss.shootSpeed, ForceMode2D.Impulse);
            bullet2.GetComponent<Bullet>().bulletDmg = boss.shootingDmg;
        }
        
        public void StartShooting()
        {
            gun.transform.localPosition = new Vector3(0, 0, 0);
            
            LeanTween.scale(gun, new Vector3(gunScale, gunScale, gunScale), 0.3f);
            LeanTween.moveLocal(gun, new Vector3(0, gunPosition, 0), 0.3f);
            StartCoroutine(ShootDelay());
        }
    
        public void EndShooting()
        {
            LeanTween.moveLocal(gun, new Vector3(0, 0, 0), 0.3f);
            LeanTween.scale(gun, new Vector3(0, 0, 0), 0.3f);

            StopAllCoroutines();
        }

    #endregion
    
    private void SpawnCoins()
    {
        var pos = transform.position;
        for (int i = 0; i < coins; i++)
        {
            Instantiate(coinObj, pos, quaternion.identity);
            pos += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }

    private void SpawnExplosions()
    {
        var exp = Instantiate(explosion, transform.position, quaternion.identity);
        Destroy(exp, 0.8f);
        cameraAnim.SetTrigger("ShakeCamBoss");
    }
}