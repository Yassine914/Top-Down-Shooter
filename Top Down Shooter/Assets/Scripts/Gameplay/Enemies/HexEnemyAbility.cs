using System.Collections;
using UnityEngine;

public class HexEnemyAbility : MonoBehaviour
{
    [SerializeField] private float enemyShootDelay;
    [SerializeField] private float enemyShootSpeed;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyLookSpd;

    [SerializeField] private float minSpreeTime;
    [SerializeField] private float maxSpreeTime;
    [SerializeField] private float minSpreeDelay;
    [SerializeField] private float maxSpreeDelay;

    private EnemyHandler _enemyHandler;

    private float _oldSpeed;
    private float _oldShootSpd;
    private float _oldShootDelay;
    private float _oldLookSpd;
    
    private void Start()
    {
        _enemyHandler = GetComponent<EnemyHandler>();
        
        _oldShootSpd = _enemyHandler.enemyShootSpeed;
        _oldSpeed = _enemyHandler.enemySpeed;
        _oldShootDelay = _enemyHandler.enemyShootDelay;
        _oldLookSpd = _enemyHandler.enemyLookSpeed;
        
        StartCoroutine(ShootingSpree());
    }

    private IEnumerator ShootingSpree()
    {
        yield return new WaitForSeconds(Random.Range(minSpreeDelay, maxSpreeDelay));
        LeanTween.color(gameObject, Color.red, 0.5f).setLoopPingPong();

        _enemyHandler.enemySpeed = enemySpeed;
        _enemyHandler.enemyShootSpeed = enemyShootSpeed;
        _enemyHandler.enemyShootDelay = enemyShootDelay;
        _enemyHandler.enemyLookSpeed = enemyLookSpd;
        
        yield return new WaitForSeconds(Random.Range(minSpreeTime, maxSpreeTime));
        LeanTween.pause(gameObject);
        LeanTween.color(gameObject, Color.white, 0.5f);

        _enemyHandler.enemySpeed = _oldSpeed;
        _enemyHandler.enemyShootSpeed = _oldShootSpd;
        _enemyHandler.enemyShootDelay = _oldShootDelay;
        _enemyHandler.enemyLookSpeed = _oldLookSpd;
        
        StartCoroutine(ShootingSpree());
    }
}