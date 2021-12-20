using UnityEngine;

public class MenuButtonTween : MonoBehaviour
{
    [SerializeField] private float positionStart;
    [SerializeField] private float positionEnd;
    [SerializeField] private float timeDelay;
    [SerializeField] private float timeDelayOut;
    
    private void Start()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(positionStart, 0, 0));
        
        LeanTween.moveX(gameObject, screen.x, .7f).setDelay(timeDelay).setEase(LeanTweenType.easeOutBack);
    }

    public void OnButtonClick()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(positionEnd, 0, 0));

        LeanTween.moveX(gameObject, screen.x, .7f).setDelay(timeDelayOut).setEase(LeanTweenType.easeInBack);
    }
}
