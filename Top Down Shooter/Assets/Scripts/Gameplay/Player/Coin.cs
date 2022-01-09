using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Vector3 player;
    private Vector2 playerPos;
    
    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position;
        playerPos = new Vector2(player.x, player.y);
        
        transform.position = Vector2.MoveTowards(transform.position, playerPos , speed * Time.deltaTime);
    }
}
