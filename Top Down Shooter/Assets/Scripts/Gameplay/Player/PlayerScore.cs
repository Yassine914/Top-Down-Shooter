using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private void OnDestroy()
    {
        FindObjectOfType<ScoreHandler>().CalculateScore();
    }
}
