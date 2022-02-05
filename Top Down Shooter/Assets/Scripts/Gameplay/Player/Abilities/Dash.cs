using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float shipSpeed;
    [SerializeField] private float abilityTime;
    [SerializeField] private Gradient startGrad;
    [SerializeField] private Gradient endGrad;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color dashColor;
    private float oldShipSpeed;
    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        oldShipSpeed = player.moveSpeed;
    }

    public void StartDash()
    {
        var trail = transform.GetChild(3).GetComponent<TrailRenderer>();
        var sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(DashAbility(trail, sprite));
    }

    private IEnumerator DashAbility(TrailRenderer trail, SpriteRenderer sprite)
    {
        trail.colorGradient = endGrad;
        player.moveSpeed = player.moveSpeed + shipSpeed;
        sprite.color = dashColor;
        Physics2D.IgnoreLayerCollision(7,8, true);
        Physics2D.IgnoreLayerCollision(7,11, true);
        
        yield return new WaitForSeconds(abilityTime);
        
        trail.colorGradient = startGrad;
        player.moveSpeed = oldShipSpeed;
        sprite.color = normalColor;
        Physics2D.IgnoreLayerCollision(7,8, false);
        Physics2D.IgnoreLayerCollision(7,11, false);
    }
}