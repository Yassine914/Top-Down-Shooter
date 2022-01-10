using UnityEngine;

public class AddCoins : MonoBehaviour
{
    private void Update()
    {
        CoinsAdd();
        ResetPlayerPrefs();
    }

    private void CoinsAdd()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100);
        }
    }

    private void ResetPlayerPrefs()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}