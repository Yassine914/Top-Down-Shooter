using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletsHandler : MonoBehaviour
{
    [SerializeField] private Bullets[] bullets;
    [SerializeField] private GameObject[] bulletsUi;
    [SerializeField] private GameObject[] selectedUi;
    [SerializeField] private GameObject[] state;
    [SerializeField] private GameObject acceptObj;
    private int selectedBullet;
    private TextMeshProUGUI buyText;
    
    private void Start()
    {
        acceptObj.SetActive(false);
        acceptObj.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (var bulletUi in bulletsUi)
            bulletUi.SetActive(false);
        
        foreach (var selected in selectedUi)
            selected.SetActive(false);
        
        selectedBullet = PlayerPrefs.GetInt("EquippedBullet", 0);

        foreach (Bullets bullet in bullets)
        {
            if (bullet.price == 0)
            {
                bullet.isUnlocked = true;
                PlayerPrefs.SetInt(bullet.bulletColor + "Unlocked", 1);

                if (bullet.index == PlayerPrefs.GetInt("EquippedBullet", 0))
                {
                    PlayerPrefs.SetInt("EquippedBullet", bullet.index);
                }
            }
            
            if (PlayerPrefs.GetInt(bullet.bulletColor + "Unlocked", 0) == 1)
            {
                bullet.isUnlocked = true;
                ChangeButton(bullet.index);
            }
            else if (PlayerPrefs.GetInt(bullet.bulletColor + "Unlocked", 0) == 0)
            {
                bullet.isUnlocked = false;
            }
        }

        selectedBullet = PlayerPrefs.GetInt("EquippedBullet", 0);
        
        bulletsUi[selectedBullet].SetActive(true);
        selectedUi[selectedBullet].SetActive(true);
        buyText.text = bullets[selectedBullet].price.ToString();
        
        ChangeButton(selectedBullet);
    }

    public void NextBullet()
    {
        bulletsUi[selectedBullet].SetActive(false);
        selectedUi[selectedBullet].SetActive(false);
        selectedBullet++;

        if (selectedBullet == bulletsUi.Length)
            selectedBullet = 0;
        
        bulletsUi[selectedBullet].SetActive(true);
        selectedUi[selectedBullet].SetActive(true);

        buyText.text = bullets[selectedBullet].price.ToString();
        ChangeButton(selectedBullet);
    }
    
    public void PreviousBullet()
    {
        bulletsUi[selectedBullet].SetActive(false);
        selectedUi[selectedBullet].SetActive(false);
        selectedBullet--;

        if (selectedBullet < 0)
            selectedBullet = bulletsUi.Length - 1;
        
        bulletsUi[selectedBullet].SetActive(true);
        selectedUi[selectedBullet].SetActive(true);
        
        buyText.text = bullets[selectedBullet].price.ToString();
        ChangeButton(selectedBullet);
    }

    private void ChangeButton(int selected)
    {
        if (PlayerPrefs.GetInt(bullets[selected].bulletColor + "Unlocked") == 0 && bullets[selected].index != PlayerPrefs.GetInt("EquippedBullet"))
        {
            state[0].SetActive(true);
            state[1].SetActive(false);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(bullets[selected].bulletColor + "Unlocked") == 1 && bullets[selected].index != PlayerPrefs.GetInt("EquippedBullet"))
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(bullets[selected].bulletColor + "Unlocked") == 1 && bullets[selected].index == PlayerPrefs.GetInt("EquippedBullet"))
        {
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);   
        }
        
        if (bullets[selectedBullet].price > PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = false;
        }
        else if (bullets[selectedBullet].price <= PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = true;
        }
    }

    public void Equip()
    {
        foreach (var ship in bullets)
            ship.isEquipped = false;
        
        bullets[selectedBullet].isEquipped = true;
        PlayerPrefs.SetInt("EquippedBullet", bullets[selectedBullet].index);
        
        ChangeButton(selectedBullet);
    }

    public void OpenAcceptBox()
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
    
    public void Buy()
    {
        bullets[selectedBullet].isUnlocked = true;
        PlayerPrefs.SetInt(bullets[selectedBullet].bulletColor + "Unlocked", 1);
        ChangeButton(selectedBullet);
        
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - bullets[selectedBullet].price);
        
        CloseAcceptBox();
    }
    
    public void CloseAcceptBox()
    {
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