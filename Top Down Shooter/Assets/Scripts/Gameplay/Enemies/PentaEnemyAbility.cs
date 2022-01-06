using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PentaEnemyAbility : MonoBehaviour
{
    [SerializeField] private GameObject shieldObj;
    [SerializeField] private float minWaitTime = 4f;
    [SerializeField] private float maxWaitTime = 6f;
    [SerializeField] private float minShieldTime = 4f;
    [SerializeField] private float maxShieldTime = 6f;
    [HideInInspector] public bool shieldIsActive;

    private void Start()
    {
        if (shieldObj.activeSelf)
            shieldObj.SetActive(false);
        
        StartCoroutine(ShieldDelay());
    }

    private void Update()
    {
        shieldIsActive = shieldObj.activeSelf;
    }

    private IEnumerator ShieldDelay()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        
        LeanTween.scale(shieldObj, new Vector3(1.8f, 1.8f, 1.8f), 0.5f);
        shieldObj.SetActive(true);
        
        yield return new WaitForSeconds(Random.Range(minShieldTime, maxShieldTime));
        
        LeanTween.scale(shieldObj, new Vector3(1.3f, 1.3f, 1.3f), 0.5f);
        
        yield return new WaitForSeconds(0.5f);
        
        shieldObj.SetActive(false);
        StartCoroutine(ShieldDelay());
    }
}
