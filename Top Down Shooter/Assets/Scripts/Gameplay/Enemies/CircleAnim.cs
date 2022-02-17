using System.Collections;
using UnityEngine;

public class CircleAnim : MonoBehaviour
{
    [SerializeField] private float scale;
    
    private SpriteRenderer sprite;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        sprite.color = new Color(255, 255, 255, 0);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        StartCoroutine(CircleTween());
    }

    private IEnumerator CircleTween()
    {
        LeanTween.value(gameObject, 0f, 1f, 0.2f).setOnUpdate((float val) =>
        {
            Color c = sprite.color;
            c.a = val;
            sprite.color = c;
        });

        yield return new WaitForSeconds(0.2f);

        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.4f);
        
        LeanTween.value(gameObject, 1f, 0f, 0.4f).setOnUpdate((float val) =>
        {
            Color c = sprite.color;
            c.a = val;
            sprite.color = c;
        });

        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}