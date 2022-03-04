using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private string modeName;
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject newHighScore;
    [SerializeField] private TextMeshProUGUI enemiesText, wavesSurvivedText, timeText, bossesKilledText, totalScoreText;
    private int enemiesKilled, wavesSurvived, time, bossesKilled, totalScore;
    private EnemyWaveHandler _waveHandler;

    private void Awake()
    {
        menu.SetActive(false);
        menu.transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        
        newHighScore.SetActive(false);
        newHighScore.transform.localScale = new Vector3(0, 0, 0);
        newHighScore.transform.rotation = new Quaternion(0, 0, 0, 0);

        _waveHandler = FindObjectOfType<EnemyWaveHandler>();
    }

    private void Update()
    {
        if(FindObjectsOfType<EnemyWaveHandler>().Length == 0) return;
        
        wavesSurvived = _waveHandler.wavesNo;
        enemiesKilled = _waveHandler.enemiesKilled;
        bossesKilled = _waveHandler.bossesKilled;
        time = _waveHandler.time;

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt(modeName, 0);
        }
    }

    public void CalculateScore()
    {
        if(bossesKilled != 0) 
            totalScore = (int) ((wavesSurvived * 3 + enemiesKilled + time * 0.3) * bossesKilled * scoreMultiplier);
        else
            totalScore = (int) ((wavesSurvived * 3 + enemiesKilled + time * 0.3) * scoreMultiplier);
        
        wavesSurvivedText.text = wavesSurvived.ToString();
        enemiesText.text = enemiesKilled.ToString();
        bossesKilledText.text = bossesKilled.ToString();

        var minutes = ((int) time / 60).ToString();
        var seconds = ((int) time % 60).ToString("00");

        timeText.text = minutes + ":" + seconds;
        
        totalScoreText.text = totalScore.ToString();
        
        if(menu != null) menu.SetActive(true);
        
        LeanTween.value(menu, 0f, 0.65f, 0.4f).setOnUpdate((float val) =>
        {
            Image r = menu.GetComponent<Image>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

        LeanTween.scale(menu.transform.GetChild(0).gameObject, new Vector3(1, 1, 1), 0.4f).setDelay(0.4f);

        StartCoroutine(AnimTrigger());

        if (totalScore > PlayerPrefs.GetInt(modeName, 0))
        {
            PlayerPrefs.SetInt(modeName, totalScore);
            Debug.Log("HighScore, "+ modeName +": "+ totalScore);
            NewHighScore();
        }
    }

    private IEnumerator AnimTrigger()
    {
        yield return new WaitForSeconds(1.2f);
        menu.GetComponent<Animator>().SetTrigger("TriggerText");
    }

    private void NewHighScore()
    {
        newHighScore.SetActive(true);
        LeanTween.scale(newHighScore, new Vector3(1, 1, 1), 0.3f).setDelay(0.8f);
        LeanTween.scale(newHighScore, new Vector3(1.1f, 1.1f, 1.1f), 1.2f).setDelay(1.2f).setLoopPingPong();
        LeanTween.rotate(newHighScore, new Vector3(0, 0, -7), 0.1f);
    }
}