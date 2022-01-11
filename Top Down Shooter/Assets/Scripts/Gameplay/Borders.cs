using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private GameObject[] borders;

    private void Awake()
    {
        if (Camera.main is null) return;
        var screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
            Screen.height, 0));

        var positionX = screenPos.x;
        var positionY = screenPos.y;
        
        var top = borders[0];
        var bottom = borders[1];
        var right = borders[2];
        var left = borders[3];
        
        LeanTween.moveX(right, positionX, 0.4f);
        LeanTween.moveX(left, - positionX, 0.4f);
        LeanTween.moveY(top, positionY, 0.4f);
        LeanTween.moveY(bottom, -positionY, 0.4f);
    }
}
