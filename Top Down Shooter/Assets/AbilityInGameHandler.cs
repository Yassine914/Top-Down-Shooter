using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInGameHandler : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private float abilityChargeTime;
    private float abilityCharge;

    private void Start()
    {
        abilityCharge = abilityChargeTime;
        StartAbility();
    }

    private void StartAbility()
    {
        abilityImage.fillAmount = 0;
    }

    private void Update()
    {
        Ability();
    }

    private void Ability()
    {
        abilityChargeTime = 1;
        abilityChargeTime += Time.deltaTime;
        
        //var abilityScaledTime = Mathf.InverseLerp(0, abilityCharge, abilityChargeTime);
        //abilityImage.fillAmount = abilityScaledTime;
        Debug.Log(abilityChargeTime);
    }
}
