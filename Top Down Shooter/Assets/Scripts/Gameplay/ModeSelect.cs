using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] private Mode[] modes;

    public void SelectMode(int modeIndex)
    {
        PlayerPrefs.SetInt("SelectedMode", modeIndex);
    }
}
