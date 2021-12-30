using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform ShootPoint1;
        [SerializeField] private Transform ShootPoint2;
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
            yield return new WaitForSeconds(0.1f);
            isShooting = false;

        }

        private void Shoot()
        {
            GameObject bullet1 = Instantiate(bulletPrefab, ShootPoint1.position, ShootPoint1.rotation);
            Rigidbody2D bulletRb1 = bullet1.GetComponent<Rigidbody2D>();
            bulletRb1.AddForce(ShootPoint1.up * bulletForce, ForceMode2D.Impulse);
        
            GameObject bullet2 = Instantiate(bulletPrefab, ShootPoint2.position, ShootPoint2.rotation);
            Rigidbody2D bulletRb2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRb2.AddForce(ShootPoint2.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
