using TMPro;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesModeEasy, wavesModeHard;
    [SerializeField] private string wavesEasy, wavesHard;

    private void Start()
    {
        wavesModeEasy.text = PlayerPrefs.GetInt(wavesEasy, 0).ToString();
        wavesModeHard.text = PlayerPrefs.GetInt(wavesHard, 0).ToString();
    }
}
