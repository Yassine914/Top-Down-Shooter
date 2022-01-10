using UnityEngine;

public class SpaceshipMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;
    private int shipType;
    private int shipColor;
    
    void Start()
    {
        foreach (var ship in ships)
        {
            ship.SetActive(false);
        }
        
        shipType = PlayerPrefs.GetInt("EquippedShip", 0);
        
        ships[shipType].SetActive(true);

        shipColor = PlayerPrefs.GetInt("EquippedColor", 0);

        foreach (var ship in ships)
        {
            var child = 0;
            for (int i = 0; i < 5; i++)
            {
                ship.transform.GetChild(child).gameObject.SetActive(false);
                child++;
            }
        }
        
        ships[shipType].transform.GetChild(shipColor).gameObject.SetActive(true);
    }
}
