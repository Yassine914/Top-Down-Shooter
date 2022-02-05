using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldObj;
    [SerializeField] private Color[] shipColors;
    [SerializeField] private float abilityTime;

    private void Start()
    {
        shieldObj.GetComponent<SpriteRenderer>().color = shipColors[PlayerPrefs.GetInt("EquippedColor")];
        shieldObj.SetActive(false);
        shieldObj.transform.localScale = new Vector3(0, 0, 0);
    }

    public void StartShield()
    {
        StartCoroutine(ShieldDelay());
    }
    private IEnumerator ShieldDelay()
    {
        LeanTween.scale(shieldObj, new Vector3(2.1423f, 2.1423f, 2.1423f), 0.5f);
        shieldObj.SetActive(true);
        
        yield return new WaitForSeconds(abilityTime);
        
        LeanTween.scale(shieldObj, new Vector3(1f, 1f, 1f), 0.5f);
        yield return new WaitForSeconds(0.5f);
        shieldObj.SetActive(false);
    }
}
