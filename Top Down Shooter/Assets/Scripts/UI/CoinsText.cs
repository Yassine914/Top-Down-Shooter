using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private int coins;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }
}
