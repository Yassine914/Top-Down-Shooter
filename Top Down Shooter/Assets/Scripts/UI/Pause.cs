using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseColors;
    [SerializeField] private GameObject pauseScreen;
    private bool isPaused;

    private void Start()
    {
        foreach (var pauseColor in pauseColors)
        {
            pauseColor.SetActive(false);
        }
        
        pauseColors[PlayerPrefs.GetInt("EquippedBullet", 0)].SetActive(true);

        isPaused = false;
        
        if (!pauseScreen.activeSelf) return;
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }
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
        isPaused = true;
        Time.timeScale = 0f;
    }

    private void PauseEnd()
    {
        ResetTime();

        if (GameObject.FindGameObjectsWithTag("Prompt").Length > 0)
        {
            if(GameObject.FindGameObjectWithTag("Prompt").activeSelf)
                GameObject.FindGameObjectWithTag("Prompt").SetActive(false);
        }
        
        isPaused = false;
        pauseScreen.SetActive(false);
    }
    
    public void ResetTime()
    {
        Time.timeScale = 1f;
    }
}
