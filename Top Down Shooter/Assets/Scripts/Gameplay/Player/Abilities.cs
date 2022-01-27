using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField] private GameObject[] abilities;

    private void Awake()
    {
        var selectedAbility = PlayerPrefs.GetInt("SelectedAbility");

        if (selectedAbility == 0)
        {
            foreach (var ability in abilities)
            {
                ability.SetActive(false);
            }
        }
        else
        {
            foreach (var ability in abilities)
            {
                ability.SetActive(false);
            }
            
            abilities[selectedAbility - 1].SetActive(true);
        }
        
        PlayerPrefs.SetInt("SelectedAbility", 0);
    }
}