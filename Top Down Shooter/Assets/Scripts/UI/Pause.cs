using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseColors;

    private void Start()
    {
        foreach (var pauseColor in pauseColors)
        {
            pauseColor.SetActive(false);
        }
        
        pauseColors[PlayerPrefs.GetInt("EquippedBullet", 0)].SetActive(true);
    }
}
