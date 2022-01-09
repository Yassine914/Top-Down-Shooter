using UnityEngine;

public class MenuButtonTween : MonoBehaviour
{
    private enum MoveAxis {  XAxis, YAxis }

    [SerializeField] private MoveAxis moveAxis;
    
    [SerializeField] private float positionStart;
    [SerializeField] private float positionEnd;
    [SerializeField] private float timeDelay;
    [SerializeField] private float timeDelayOut;

    private void Start()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(positionStart, positionStart, 0));

        if (moveAxis == MoveAxis.XAxis)
            LeanTween.moveX(gameObject, screen.x, .7f).setDelay(timeDelay).setEase(LeanTweenType.easeOutBack);
        else if (moveAxis == MoveAxis.YAxis)
            LeanTween.moveY(gameObject, screen.y, .7f).setDelay(timeDelay).setEase(LeanTweenType.easeOutBack);
    }

    public void OnButtonClick()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(positionEnd, positionEnd, 0));

        if (moveAxis == MoveAxis.XAxis)
            LeanTween.moveX(gameObject, screen.x, .7f).setDelay(timeDelayOut).setEase(LeanTweenType.easeInBack);
        else if(moveAxis == MoveAxis.YAxis)
            LeanTween.moveY(gameObject, screen.y, .7f).setDelay(timeDelayOut).setEase(LeanTweenType.easeInBack);
    }
}
