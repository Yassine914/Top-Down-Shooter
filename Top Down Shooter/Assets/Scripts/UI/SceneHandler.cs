using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private float sceneDelay;
    
    public void PlayButton()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(PlayButtonDelay(currentScene));
    }

    private IEnumerator PlayButtonDelay(int currentScene)
    {
        yield return new WaitForSeconds(sceneDelay);
        SceneManager.LoadScene(currentScene + 1);
    }

    public void ShopButton()
    {
        StartCoroutine(ShopButtonDelay());
    }

    private IEnumerator ShopButtonDelay()
    {
        yield return new WaitForSeconds(sceneDelay);
        SceneManager.LoadScene("Shop");
    }
    
    public void SettingsButton()
    {
        StartCoroutine(SettingsButtonDelay());
    }
    
    private IEnumerator SettingsButtonDelay()
    {
        yield return new WaitForSeconds(sceneDelay);
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
