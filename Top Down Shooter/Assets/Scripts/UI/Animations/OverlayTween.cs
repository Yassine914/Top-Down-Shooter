using UnityEngine;

public class OverlayTween : MonoBehaviour
{
    private void Start()
    {
        LeanTween.value(gameObject, 0f, 0.5f, 1f).setOnUpdate((float val) =>
        {
            UnityEngine.UI.RawImage r = gameObject.GetComponent<UnityEngine.UI.RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
    }
}
