using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInGameHandler : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private float abilityChargeTime;
    [SerializeField] private float abilityActiveTime;
    private float abilityCharge;
    [HideInInspector] public bool isAbilityActive;

    private void Start()
    {
        isAbilityActive = false;
        abilityCharge = abilityChargeTime;
        StartAbility();
    }

    private void StartAbility()
    {
        abilityImage.fillAmount = 1;
    }
    
    private void Update()
    {
        Ability();
    }

    private void Ability()
    {
        if (abilityChargeTime > 0 && !isAbilityActive)
        {
            abilityChargeTime -= Time.deltaTime;
            GetComponent<Button>().interactable = false;
            
            var abilityScaledTime = Mathf.InverseLerp(0, abilityCharge, abilityChargeTime);
            abilityImage.fillAmount = abilityScaledTime;
        }
        else if (abilityChargeTime <= 0)
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void AbilityPress(int abilityIndex)
    {
        GetComponent<Button>().interactable = false;
        abilityChargeTime = abilityCharge;
        abilityImage.fillAmount = 1;
        StartCoroutine(AbilityActiveTime());

        switch (abilityIndex)
        {
            case 1:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shield>().StartShield();
                Debug.Log("Shield Started");
                break;
            case 2:
                GameObject.FindGameObjectWithTag("Player").GetComponent<BulletSpree>().StartBulletSpree();
                break;
            case 3:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Dash>().StartDash();
                break;
        }
    }

    private IEnumerator AbilityActiveTime()
    {
        isAbilityActive = true;
        yield return new WaitForSeconds(abilityActiveTime);
        isAbilityActive = false;
    }
}
