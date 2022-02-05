using System.Collections;
using UnityEngine;

public class BulletSpree : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDelay;
    [SerializeField] private float abilityTime;
    private float oldBulletSpeed;
    private float oldBulletDelay;
    private PlayerShooting playerShooting;

    private void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        
        oldBulletSpeed = playerShooting.bulletForce;
        oldBulletDelay = playerShooting.bulletDelay;
    }

    public void StartBulletSpree()
    {
        StartCoroutine(BulletSpreeDelay());
    }

    private IEnumerator BulletSpreeDelay()
    {
        playerShooting.bulletForce = bulletSpeed;
        playerShooting.bulletDelay = bulletDelay;
        yield return new WaitForSeconds(abilityTime);
        playerShooting.bulletForce = oldBulletSpeed;
        playerShooting.bulletDelay = oldBulletDelay;
    }
}
