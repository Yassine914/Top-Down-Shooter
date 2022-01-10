using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorsHandler : MonoBehaviour
{
    [SerializeField] private Colors[] colors;
    [SerializeField] private GameObject[] colorsUi;
    [SerializeField] private GameObject[] selectedUi;
    [SerializeField] private GameObject[] state;
    private int selectedColor;
    private TextMeshProUGUI buyText;
    
    private void Start()
    {
        buyText = state[0].GetComponentInChildren<TextMeshProUGUI>();
        
        foreach (var colorUi in colorsUi)
            colorUi.SetActive(false);
        
        foreach (var selected in selectedUi)
            selected.SetActive(false);

        foreach (Colors color in colors)
        {
            if (color.price == 0)
            {
                color.isUnlocked = true;
                PlayerPrefs.SetInt(color.color + "Unlocked", 1);

                if (color.index == PlayerPrefs.GetInt("EquippedColor", 0))
                {
                    PlayerPrefs.SetInt("EquippedColor", color.index);
                }
            }
            
            if (PlayerPrefs.GetInt(color.color + "Unlocked", 0) == 1)
            {
                color.isUnlocked = true;
                ChangeButton(color.index);
            }
            else if (PlayerPrefs.GetInt(color.color + "Unlocked", 0) == 0)
            {
                color.isUnlocked = false;
            }
        }
        
        selectedColor = PlayerPrefs.GetInt("EquippedColor", 0);

        colorsUi[selectedColor].SetActive(true);
        selectedUi[selectedColor].SetActive(true);
        buyText.text = colors[selectedColor].price.ToString();
        
        ChangeButton(selectedColor);
    }

    public void NextColor()
    {
        colorsUi[selectedColor].SetActive(false);
        selectedUi[selectedColor].SetActive(false);
        selectedColor++;

        if (selectedColor == colorsUi.Length)
            selectedColor = 0;
        
        colorsUi[selectedColor].SetActive(true);
        selectedUi[selectedColor].SetActive(true);

        buyText.text = colors[selectedColor].price.ToString();
        ChangeButton(selectedColor);
    }
    
    public void PreviousColor()
    {
        colorsUi[selectedColor].SetActive(false);
        selectedUi[selectedColor].SetActive(false);
        selectedColor--;

        if (selectedColor < 0)
            selectedColor = colorsUi.Length - 1;
        
        colorsUi[selectedColor].SetActive(true);
        selectedUi[selectedColor].SetActive(true);
        
        buyText.text = colors[selectedColor].price.ToString();
        ChangeButton(selectedColor);
    }

    private void ChangeButton(int selected)
    {
        if (PlayerPrefs.GetInt(colors[selected].color + "Unlocked") == 0 && colors[selected].index != PlayerPrefs.GetInt("EquippedColor"))
        {
            state[0].SetActive(true);
            state[1].SetActive(false);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(colors[selected].color + "Unlocked") == 1 && colors[selected].index != PlayerPrefs.GetInt("EquippedColor"))
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
            state[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(colors[selected].color + "Unlocked") == 1 && colors[selected].index == PlayerPrefs.GetInt("EquippedColor"))
        {
            state[0].SetActive(false);
            state[1].SetActive(false);
            state[2].SetActive(true);   
        }
        
        if (colors[selectedColor].price > PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = false;
        }
        else if (colors[selectedColor].price <= PlayerPrefs.GetInt("Coins", 0))
        {
            state[0].GetComponent<Button>().interactable = true;
        }
    }

    public void Equip()
    {
        foreach (var ship in colors)
            ship.isEquipped = false;
        
        colors[selectedColor].isEquipped = true;
        PlayerPrefs.SetInt("EquippedColor", colors[selectedColor].index);
        
        ChangeButton(selectedColor);
    }

    public void Buy()
    {
        colors[selectedColor].isUnlocked = true;
        PlayerPrefs.SetInt(colors[selectedColor].color + "Unlocked", 1);
        ChangeButton(selectedColor);
        
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - colors[selectedColor].price);
    }
}
