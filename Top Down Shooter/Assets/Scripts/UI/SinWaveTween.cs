using UnityEngine;

public class SinWaveTween : MonoBehaviour
{
    [SerializeField] private float position;
    [SerializeField] private float tweenTime;
    
    private void Start()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(0, position, 0));
        LeanTween.moveY(gameObject, screen.y, tweenTime).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
    }

    public void OnButtonClick()
    {
        LeanTween.pause(gameObject);
        if (LeanTween.isPaused(gameObject))
        {
            GetComponent<Tween>().TweenMove();
        }
    }
}
