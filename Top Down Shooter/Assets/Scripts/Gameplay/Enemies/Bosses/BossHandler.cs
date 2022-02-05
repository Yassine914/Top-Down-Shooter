using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossHandler : MonoBehaviour
{
    private enum PhaseType
    {
        ShootOnly, RunOnly, EarthquakeOnly, SpawnOnly,
        ShootAndRun, ShootAndShield, RunAndSpawn, ShootAndSpawn, ShootAndEarthquake
    }
    
    [SerializeField] private Boss boss;
    [SerializeField] private SpriteRenderer bodySprite, gunSprite;
    [SerializeField] private PhaseType[] phases;
    private GameObject player;
    private Slider hpBarSlider;
    private TextMeshProUGUI healthText;
    private int health;
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint1, shootPoint2;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        hpBarSlider = GameObject.FindGameObjectWithTag("BossHpBar").GetComponent<Slider>();
        healthText = hpBarSlider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        health = boss.health;
        hpBarSlider.maxValue = health;
        
        healthText.text = health.ToString();
    }

    private void Start()
    {
        var color = Random.Range(0, 17);
        bodySprite.sprite = boss.bodySprites[color];
        gunSprite.sprite = boss.gunSprites[color];
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
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            healthText.text = "0";
            gameObject.SetActive(false);
            //Spawn Explosions
            Destroy(gameObject, 0.5f);
        }
    }

    private void MoveAround()
    {
        var playerLoc = player.transform.position;
        var spawnLoc = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));

        if (spawnLoc.x < playerLoc.x + 1 && spawnLoc.x > playerLoc.x - 1 && spawnLoc.y < playerLoc.y + 1 && spawnLoc.y > playerLoc.y - 1)
            MoveAround();
        else
            transform.position = Vector2.MoveTowards(transform.position, spawnLoc , boss.moveSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * boss.lookSpeed);
    }
    
    private void SpawnEnemies()
    {
        var playerLoc = player.transform.position;
        var spawnLoc = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));

        if (spawnLoc.x < playerLoc.x + 1 && spawnLoc.x > playerLoc.x - 1 && 
            spawnLoc.y < playerLoc.y + 1 && spawnLoc.y > playerLoc.y - 1)
            SpawnEnemies();
        else
            Instantiate(enemyObj, spawnLoc, quaternion.identity);
    }
    
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
}