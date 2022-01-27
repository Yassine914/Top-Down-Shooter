using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenuTween : MonoBehaviour
{
    [SerializeField] private GameObject infoObj;

    private void Start()
    {
        infoObj.transform.GetChild(0).localScale = new Vector3(0, 0, 0);
        
        if(!infoObj.activeSelf) return;
        infoObj.SetActive(false);
    }

    public void InfoMenuStart()
    {
        infoObj.SetActive(true);
        
        LeanTween.value(infoObj, 0f, 0.6f, 0.3f).setOnUpdate(val =>
        {
            RawImage r = infoObj.GetComponent<RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });

        LeanTween.scale((RectTransform) infoObj.transform.GetChild(0).transform, new Vector3(1, 1, 1), 0.3f);
    }

    public void InfoMenuEnd()
    {
        LeanTween.value(infoObj, 0.6f, 0f, 0.3f).setOnUpdate((val) =>
        {
            RawImage r = infoObj.GetComponent<RawImage>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        
        LeanTween.scale((RectTransform) infoObj.transform.GetChild(0).transform, new Vector3(0, 0, 0), 0.3f);
        StartCoroutine(InfoEndDelay());
    }

    private IEnumerator InfoEndDelay()
    {
        yield return new WaitForSeconds(0.3f);
        infoObj.SetActive(false);
    }
}
