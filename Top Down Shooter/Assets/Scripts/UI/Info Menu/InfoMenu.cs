using TMPro;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] private ModeInfo modeInfo;
    [SerializeField] private TextMeshProUGUI infoTitle;
    [SerializeField] private TextMeshProUGUI infoDescription;

    public void ChangeText()
    {
        infoTitle.text = modeInfo.modeName;
        infoDescription.text = modeInfo.modeInfo;
    }
    
    public void OpenInfoMenu()
    {
        LeanTween.value(gameObject, 0f, 0.6f, 1f).setOnUpdate((float val) =>
        {
            UnityEngine.UI.RawImage r = gameObject.GetComponent<UnityEngine.UI.RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
    }
}