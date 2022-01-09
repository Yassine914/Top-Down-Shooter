using UnityEngine;

public class TransitionTween : MonoBehaviour
{
    [SerializeField] private float startPosition;
    [SerializeField] private float timeDelay;
    [SerializeField] private float tweenTime;

    private void Start()
    {
        var screen = Camera.main.WorldToScreenPoint(new Vector3(0, startPosition, 0));
        LeanTween.moveY(gameObject, screen.y, tweenTime).setDelay(timeDelay);
    }
}
