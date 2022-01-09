using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipsHandler : MonoBehaviour
{
    [SerializeField] private Ships[] ships;
    [SerializeField] private GameObject[] shipsUi;
    [SerializeField] private GameObject[] state;
    private int selectedShip;
    private TextMeshProUGUI buyText;

    private void Start()
    {
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (var shipUi in shipsUi)
            shipUi.SetActive(false);

        foreach (Ships ship in ships)
        {
            if (ship.price == 0)
            {
                ship.isUnlocked = true;

                state[0].SetActive(false);
                state[1].SetActive(true);
                state[2].SetActive(false);

                if (ship.isEquipped)
                {
                    state[0].SetActive(false);
                    state[1].SetActive(false);
                    state[2].SetActive(true);
                }
            }
        }

        shipsUi[selectedShip].SetActive(true);
        
        buyText.text = ships[selectedShip].price.ToString();
    }

    public void NextShip()
    {
        shipsUi[selectedShip].SetActive(false);
        selectedShip++;

        if (selectedShip == shipsUi.Length)
            selectedShip = 0;
        
        shipsUi[selectedShip].SetActive(true);

        buyText.text = ships[selectedShip].price.ToString();
        ChangeButton(selectedShip);
    }
    
    public void PreviousShip()
    {
        shipsUi[selectedShip].SetActive(false);
        selectedShip--;

        if (selectedShip < 0)
            selectedShip = shipsUi.Length - 1;
        
        shipsUi[selectedShip].SetActive(true);
        
        buyText.text = ships[selectedShip].price.ToString();
        ChangeButton(selectedShip);
    }

    private void ChangeButton(int selected)
    {
        if (ships[selected].price == 0)
        {
            ships[selected].isUnlocked = true;
                
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);

            if (!ships[selected].isEquipped) return;
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);
        }
        else if (ships[selected].isUnlocked && !ships[selected].isEquipped)
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);
        }
        else if (ships[selected].isUnlocked && ships[selected].isEquipped)
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
        
        if (ships[selectedShip].price > PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = false;
        }
    }

    public void Equip()
    {
        foreach (var ship in ships)
            ship.isEquipped = false;
        
        ships[selectedShip].isEquipped = true;
        ChangeButton(selectedShip);
    }

    public void Buy()
    {
        ships[selectedShip].isUnlocked = true;
        ChangeButton(selectedShip);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - ships[selectedShip].price);
    }
}