using TMPro;
using UnityEngine;

public class BulletsHandler : MonoBehaviour
{
    [SerializeField] private Bullets[] bullets;
    [SerializeField] private GameObject[] bulletsUi;
    [SerializeField] private GameObject[] state;
    private int selectedBullet;
    private TextMeshProUGUI buyText;

    private void Start()
    {
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (var shipUi in bulletsUi)
            shipUi.SetActive(false);

        foreach (Bullets bullet in bullets)
        {
            if (bullet.price == 0)
            {
                bullet.isUnlocked = true;

                state[0].SetActive(false);
                state[1].SetActive(true);
                state[2].SetActive(false);

                if (bullet.isEquipped)
                {
                    state[0].SetActive(false);
                    state[1].SetActive(false);
                    state[2].SetActive(true);
                }
            }
        }

        bulletsUi[selectedBullet].SetActive(true);
        
        buyText.text = bullets[selectedBullet].price.ToString();
    }

    public void NextBullet()
    {
        bulletsUi[selectedBullet].SetActive(false);
        selectedBullet++;

        if (selectedBullet == bulletsUi.Length)
            selectedBullet = 0;
        
        bulletsUi[selectedBullet].SetActive(true);

        buyText.text = bullets[selectedBullet].price.ToString();
        ChangeButton(selectedBullet);
    }
    
    public void PreviousBullet()
    {
        bulletsUi[selectedBullet].SetActive(false);
        selectedBullet--;

        if (selectedBullet < 0)
            selectedBullet = bulletsUi.Length - 1;
        
        bulletsUi[selectedBullet].SetActive(true);
        
        buyText.text = bullets[selectedBullet].price.ToString();
        ChangeButton(selectedBullet);
    }

    private void ChangeButton(int selected)
    {
        if (bullets[selected].price == 0)
        {
            bullets[selected].isUnlocked = true;
                
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);

            if (!bullets[selected].isEquipped) return;
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);
        }
        else if (bullets[selected].isUnlocked && !bullets[selected].isEquipped)
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);
        }
        else if (bullets[selected].isUnlocked && bullets[selected].isEquipped)
        {
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);
        }
        else
        {
            state[0].SetActive(true);
            state[1].SetActive(false);
            state[2].SetActive(false);
        }
    }

    public void Equip()
    {
        foreach (var bullet in bullets)
            bullet.isEquipped = false;
        
        bullets[selectedBullet].isEquipped = true;
        ChangeButton(selectedBullet);
    }
}
