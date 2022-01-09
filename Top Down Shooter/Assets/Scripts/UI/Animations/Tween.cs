using UnityEngine;

public class Tween : MonoBehaviour
{
    private enum MoveAxis {  XAxis, YAxis }

    [SerializeField] private MoveAxis moveAxis;
    [SerializeField] private float movePosition;
    [SerializeField] private float tweenTime;
    [SerializeField] private float delayTime;
    private Vector3 screen;

    private void Start()
    {
        screen = Camera.main.WorldToScreenPoint(new Vector3(movePosition, movePosition, 0));
    }

    public void TweenMove()
    {
        if (moveAxis == MoveAxis.XAxis)
            LeanTween.moveX(gameObject, screen.x, tweenTime).setDelay(delayTime).setEase(LeanTweenType.easeOutBack);
        else if (moveAxis == MoveAxis.YAxis) 
            LeanTween.moveY(gameObject, screen.y, tweenTime).setDelay(delayTime).setEase(LeanTweenType.easeInBack);
    }
}
