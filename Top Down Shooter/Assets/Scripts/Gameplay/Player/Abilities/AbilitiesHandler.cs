using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilitiesHandler : MonoBehaviour
{
    [SerializeField] private Ability[] abilities;
    [SerializeField] private GameObject[] abilitiesObjs;
    [SerializeField] private GameObject acceptObj;
    private int selected;

    private void Start()
    {
        acceptObj.SetActive(false);
        acceptObj.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        
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
    }

    public void OpenAcceptBox()
    {
        if (selected == 0)
        {
            Continue();
        }
        else
        {
            acceptObj.SetActive(true);
        
            LeanTween.value(acceptObj, 0f, 0.4f, 0.3f).setOnUpdate((float val) =>
            {
                RawImage r = acceptObj.GetComponent<RawImage>();
                Color c = r.color;
                c.a = val;
                r.color = c;
            });
        
            LeanTween.scale(acceptObj.transform.GetChild(0).gameObject, new Vector3(1, 1, 1), 0.3f);
        }
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

    public void CancelPurchase()
    {
        selected = 0;
        
        foreach (var abilityObj in abilitiesObjs)
        {
            abilityObj.transform.GetChild(4).gameObject.SetActive(false);
        }
        
        LeanTween.value(acceptObj, 0.4f, 0f, 0.3f).setOnUpdate((float val) =>
        {
            RawImage r = acceptObj.GetComponent<RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        
        LeanTween.scale(acceptObj.transform.GetChild(0).gameObject, new Vector3(0, 0, 0), 0.3f);

        StartCoroutine(CancelDelay());
    }

    private IEnumerator CancelDelay()
    {
        yield return new WaitForSeconds(0.3f);
        acceptObj.SetActive(false);
    }
}