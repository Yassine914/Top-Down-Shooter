using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlanetMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private float rotation;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        direction.x = Random.Range(-0.03f, 0.04f);
        direction.y = Random.Range(-0.03f, 0.04f);
        rotation = Random.Range(-0.01f, 0.01f);

        
        rb2d.AddForce(direction, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.Rotate(0 ,0, rotation, Space.Self);
    }
}
