using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private float movementSpeed = 2f;

    [Header("Bullets")] 
    [SerializeField] private GameObject bullet;
    
    private Rigidbody2D rb2d; 

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LookAtMouse();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        rb2d.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        velocityText.text = ((int) rb2d.velocity.magnitude).ToString();
    }

    private void LookAtMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        var mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            var bulletInstance = Instantiate(bullet, mousePos, Quaternion.identity);
        }
    }
}
