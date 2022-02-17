using UnityEngine;

public class Earthquake : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
