using UnityEngine;

public class PlayerShipHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;
    [SerializeField] private ShipSprites[] shipSprites;
    private int shipType;
    private int shipColor;


    private void Awake()
    {
        shipType = PlayerPrefs.GetInt("EquippedShip", 0);
        shipColor = PlayerPrefs.GetInt("EquippedColor", 0);
        
        foreach (var ship in ships)
        {
            ship.SetActive(false);
        }

        ships[shipType].SetActive(true);

        ships[shipType].GetComponent<SpriteRenderer>().sprite = shipSprites[shipType].sprites[shipColor];
    }
}
