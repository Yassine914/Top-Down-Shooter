using UnityEngine;

public class Shop : MonoBehaviour
{
    
}

[System.Serializable]
public class Ships
{
    public string name;
    public int price;
    public GameObject prefab;
}

[System.Serializable]
public class Colors
{
    public string color;
    public int price;
}

[System.Serializable]
public class Bullets
{
    public string bulletColor;
    public int price;
    public GameObject prefab;
}