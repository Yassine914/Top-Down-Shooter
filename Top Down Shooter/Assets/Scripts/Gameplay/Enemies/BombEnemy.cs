using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemyInfo;

    [HideInInspector] public int enemyDamage;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        enemyDamage = enemyInfo.enemyDamage;
    }
    
    private void Update()
    {
        FollowPlayer(_player.position);
            
        if(enemyInfo.enemyHealth <= 0)
            Destroy(gameObject);
    }
    
    private void FollowPlayer(Vector3 playerPos)
    {
        var enemyPos = transform.position;
        
        transform.position = Vector2.MoveTowards(enemyPos,
            playerPos , enemyInfo.enemySpeed * Time.deltaTime);

            Vector3 dir = (playerPos - enemyPos);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * enemyInfo.enemyLookSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullets"))
        {
            enemyInfo.enemyHealth--;
        }

        if (other.collider.CompareTag("Player"))
        {
            enemyInfo.enemyHealth -= enemyInfo.enemyHealth;
            other.gameObject.GetComponent<PlayerHealth>().health -= enemyDamage;
        }
    }
}
