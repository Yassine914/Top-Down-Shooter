using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private Transform ShootPoint1;
    [SerializeField] private Transform ShootPoint2;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] public float bulletForce;
    [SerializeField] public float bulletDelay;
    private bool isShooting = false;
    private GameObject selectedBullet;


    private void Awake()
    {
        selectedBullet = bullets[PlayerPrefs.GetInt("EquippedBullet", 0)];
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && !isShooting)
        {
            StartCoroutine(ShootDelay());
        }
    }
    
    private IEnumerator ShootDelay()
    {
        Shoot();
        isShooting = true;
        yield return new WaitForSeconds(bulletDelay);
        isShooting = false;
    }

    private void Shoot()
    {
        GameObject bullet1 = Instantiate(selectedBullet, ShootPoint1.position, ShootPoint1.rotation);
        Rigidbody2D bulletRb1 = bullet1.GetComponent<Rigidbody2D>();
        bulletRb1.AddForce(ShootPoint1.up * bulletForce, ForceMode2D.Impulse);
        
        GameObject bullet2 = Instantiate(selectedBullet, ShootPoint2.position, ShootPoint2.rotation);
        Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
        bulletRb2.AddForce(ShootPoint2.up * bulletForce, ForceMode2D.Impulse);
    }
}