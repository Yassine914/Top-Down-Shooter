using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        Destroy(gameObject);
    }
}
