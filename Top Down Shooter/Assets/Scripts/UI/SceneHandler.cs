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

    public void BackToMenu()
    {
        StartCoroutine(BackToMenuDelay());
    }
    
    private IEnumerator BackToMenuDelay()
    {
        yield return new WaitForSeconds(sceneDelay);
        SceneManager.LoadScene("Main Menu");
    }
    
    public void EasyMode()
    {
        SceneManager.LoadScene("Wave Mode Easy");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("Wave Mode Hard");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial Mode");
    }

    public void BossesMode()
    {
        SceneManager.LoadScene("Bosses Mode");
    }

    public void AbilitySelect()
    {
        SceneManager.LoadScene("Abilities Select");
    }
}