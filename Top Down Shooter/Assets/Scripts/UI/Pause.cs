using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseColors;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private float delay;
    private bool isPaused;

    private void Start()
    {
        foreach (var pauseColor in pauseColors)
        {
            pauseColor.SetActive(false);
        }
        
        pauseColors[PlayerPrefs.GetInt("EquippedBullet", 0)].SetActive(true);

        pauseScreen.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        isPaused = false;
        
        if (!pauseScreen.activeSelf) return;
        pauseScreen.SetActive(false);
    }

    public void PauseGame()
    {
        switch (isPaused)
        {
            case false:
                PauseStart();
                break;
            case true:
                PauseEnd();
                break;
        }
    }
    
    private void PauseStart()
    {
        pauseScreen.SetActive(true);
        
        LeanTween.value(gameObject, 0f, 0.6f, delay).setOnUpdate((float val) =>
        {
            UnityEngine.UI.RawImage r = pauseScreen.GetComponent<UnityEngine.UI.RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

        LeanTween.scale((RectTransform)pauseScreen.transform.GetChild(0).transform, new Vector2(1, 1), delay);
        isPaused = true;
        StartCoroutine(PauseStartDelay());
    }

    private IEnumerator PauseStartDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0f;
    }
    
    private void PauseEnd()
    {
        ResetTime();
        LeanTween.value(gameObject, 0.6f, 0f, delay).setOnUpdate((float val) =>
        {
            UnityEngine.UI.RawImage r = pauseScreen.GetComponent<UnityEngine.UI.RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        
        LeanTween.scale((RectTransform)pauseScreen.transform.GetChild(0).transform, new Vector2(0, 0), delay);
        isPaused = false;
        StartCoroutine(PauseEndDelay());
    }
    
    private IEnumerator PauseEndDelay()
    {
        yield return new WaitForSecondsRealtime(0.001f);
        pauseScreen.SetActive(false);
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
    }
}
