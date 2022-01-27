using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilitiesHandler : MonoBehaviour
{
    [SerializeField] private Ability[] abilities;
    [SerializeField] private GameObject[] abilitiesObjs;
    private int selected;

    private void Start()
    {
        foreach (var abilityObj in abilitiesObjs)
        {
            abilityObj.transform.GetChild(4).gameObject.SetActive(false);
        }

        foreach (var ability in abilities)
        {
            if(ability.abilityCost <= PlayerPrefs.GetInt("Coins", 0))
                abilitiesObjs[ability.abilityIndex - 1].GetComponent<Button>().interactable = true;
            else
                abilitiesObjs[ability.abilityIndex - 1].GetComponent<Button>().interactable = false;
        }
    }

    public void AbilitySelect(int abilityIndex)
    {
        foreach (var abilityObj in abilitiesObjs)
        {
            abilityObj.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (abilityIndex != selected)
        {
            abilitiesObjs[abilityIndex - 1].transform.GetChild(4).gameObject.SetActive(true);
            selected = abilityIndex;
        }
        else
        {
            selected = 0;
        }

        /*if (abilityIndex == selected)
        {
            foreach (var abilityObj in abilitiesObjs)
            {
                abilityObj.transform.GetChild(4).gameObject.SetActive(false);
            } 
            selected = 0;
        }
        else
        {
            foreach (var abilityObj in abilitiesObjs)
            {
                abilityObj.transform.GetChild(4).gameObject.SetActive(false);
            }
        
            abilitiesObjs[abilityIndex - 1].transform.GetChild(4).gameObject.SetActive(true);
        }
        
        selected = abilityIndex;*/
    }

    public void Continue()
    {
        switch (PlayerPrefs.GetInt("SelectedMode"))
        {
            case 1:
                SceneManager.LoadScene("Wave Mode Easy");
                break;
            case 2:
                SceneManager.LoadScene("Wave Mode Hard");
                break;
            case 3:
                SceneManager.LoadScene("Bosses Mode");
                break;
        }
        
        PlayerPrefs.SetInt("SelectedAbility", selected);
        if(selected != 0)
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - abilities[selected - 1].abilityCost);
        
        Debug.Log("SelectedAbility: " + selected);
    }
}