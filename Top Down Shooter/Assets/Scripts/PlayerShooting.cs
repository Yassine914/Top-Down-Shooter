using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    private bool isShooting = false;

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
        yield return new WaitForSeconds(0.000001f);
        isShooting = false;

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, ShootPoint.position, ShootPoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(ShootPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
