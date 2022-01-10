using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletsHandler : MonoBehaviour
{
    [SerializeField] private Bullets[] bullets;
    [SerializeField] private GameObject[] bulletsUi;
    [SerializeField] private GameObject[] selectedUi;
    [SerializeField] private GameObject[] state;
    private int selectedBullet;
    private TextMeshProUGUI buyText;
    
    private void Start()
    {
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (var bulletUi in bulletsUi)
            bulletUi.SetActive(false);
        
        foreach (var selected in selectedUi)
            selected.SetActive(false);

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

    public void Buy()
    {
        bullets[selectedBullet].isUnlocked = true;
        PlayerPrefs.SetInt(bullets[selectedBullet].bulletColor + "Unlocked", 1);
        ChangeButton(selectedBullet);
        
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - bullets[selectedBullet].price);
    }
}
