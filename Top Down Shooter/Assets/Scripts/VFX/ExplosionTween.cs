using Shapes;
using UnityEngine;

public class ExplosionTween : MonoBehaviour
{
    [SerializeField] private float radiusEnd;
    [SerializeField] private float thicknessEnd;
    [SerializeField] private float time;
    private Disc _disc;
    private Disc _disc1;
    private Disc _disc2;
    private Disc _disc3;

    private void Start()
    {
        _disc = GetComponent<Disc>();
        Tween();
    }
    
    private void Tween()
    {
        var radius = GetComponent<Disc>().Radius;
        var thickness = GetComponent<Disc>().Thickness;
        
        LeanTween.value(radius, radius, radiusEnd, time).setOnUpdate((float val) =>
        {
            if (_disc is null) return;
                _disc.Radius = val;
        });
        
        LeanTween.value(thickness, thickness, thicknessEnd, time).setOnUpdate((float val) =>
        {
            if (_disc is null) return;
                _disc.Thickness = val;
        });
        
        Destroy(gameObject, time + 0.2f);
    }
}
