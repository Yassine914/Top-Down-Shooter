using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform explosionLoc;
    [HideInInspector] public int bulletDmg;
    
    private Collider2D _collider2D;
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = false;
        StartCoroutine(TriggerDelay());
        _collider2D = GetComponent<Collider2D>();
    }

    IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(0.09f);
        _collider2D.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BulletDestroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Shield"))
            BulletDestroy();
    }

    private void BulletDestroy()
    {
        Instantiate(explosion, explosionLoc.position, quaternion.identity);
        Destroy(gameObject);
    }
}