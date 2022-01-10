using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipsHandler : MonoBehaviour
{
    [SerializeField] private Ships[] ships;
    [SerializeField] private GameObject[] shipsUi;
    [SerializeField] private GameObject[] selectedUi;
    [SerializeField] private GameObject[] state;
    private int selectedShip;
    private TextMeshProUGUI buyText;
    
    private void Start()
    {
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();

        foreach (var shipUi in shipsUi)
            shipUi.SetActive(false);

        foreach (var selected in selectedUi)
            selected.SetActive(false);

        selectedShip = PlayerPrefs.GetInt("EquippedShips", 0);
        
        foreach (Ships ship in ships)
        {
            if (ship.price == 0)
            {
                ship.isUnlocked = true;
                PlayerPrefs.SetInt(ship.name + "Unlocked", 1);

                if (ship.index == PlayerPrefs.GetInt("EquippedShip", 0))
                {
                    PlayerPrefs.SetInt("EquippedShip", ship.index);
                }
            }
            
            if (PlayerPrefs.GetInt(ship.name + "Unlocked", 0) == 1)
            {
                ship.isUnlocked = true;
                ChangeButton(ship.index);
            }
            else if (PlayerPrefs.GetInt(ship.name + "Unlocked", 0) == 0)
            {
                ship.isUnlocked = false;
            }
        }

        selectedShip = PlayerPrefs.GetInt("EquippedShip", 0);
        
        shipsUi[selectedShip].SetActive(true);
        selectedUi[selectedShip].SetActive(true);
        buyText.text = ships[selectedShip].price.ToString();
        
        ChangeButton(selectedShip);
    }

    public void NextShip()
    {
        shipsUi[selectedShip].SetActive(false);
        selectedUi[selectedShip].SetActive(false);
        selectedShip++;

        if (selectedShip == shipsUi.Length)
            selectedShip = 0;
        
        shipsUi[selectedShip].SetActive(true);
        selectedUi[selectedShip].SetActive(true);

        buyText.text = ships[selectedShip].price.ToString();
        ChangeButton(selectedShip);
    }
    
    public void PreviousShip()
    {
        shipsUi[selectedShip].SetActive(false);
        selectedUi[selectedShip].SetActive(false);
        selectedShip--;

        if (selectedShip < 0)
            selectedShip = shipsUi.Length - 1;
        
        shipsUi[selectedShip].SetActive(true);
        selectedUi[selectedShip].SetActive(true);
        
        buyText.text = ships[selectedShip].price.ToString();
        ChangeButton(selectedShip);
    }

    private void ChangeButton(int selected)
    {
        if (PlayerPrefs.GetInt(ships[selected].name + "Unlocked") == 0 && ships[selected].index != PlayerPrefs.GetInt("EquippedShip"))
        {
            state[0].SetActive(true);
            state[1].SetActive(false);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(ships[selected].name + "Unlocked") == 1 && ships[selected].index != PlayerPrefs.GetInt("EquippedShip"))
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(ships[selected].name + "Unlocked") == 1 && ships[selected].index == PlayerPrefs.GetInt("EquippedShip"))
        {
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);   
        }
        
        if (ships[selectedShip].price > PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = false;
        }
        else if (ships[selectedShip].price <= PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = true;
        }
    }

    public void Equip()
    {
        foreach (var ship in ships)
            ship.isEquipped = false;
        
        ships[selectedShip].isEquipped = true;
        PlayerPrefs.SetInt("EquippedShip", ships[selectedShip].index);
        
        ChangeButton(selectedShip);
    }

    public void Buy()
    {
        ships[selectedShip].isUnlocked = true;
        PlayerPrefs.SetInt(ships[selectedShip].name + "Unlocked", 1);
        ChangeButton(selectedShip);
        
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - ships[selectedShip].price);
    }
}