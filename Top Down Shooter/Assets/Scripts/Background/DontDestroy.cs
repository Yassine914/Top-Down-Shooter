using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Background").Length == 2)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
