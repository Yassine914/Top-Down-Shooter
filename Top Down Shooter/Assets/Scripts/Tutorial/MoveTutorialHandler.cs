using System.Collections;
using UnityEngine;

public class MoveTutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject target1, target2;
    [SerializeField] private GameObject moveToTargetText;
    [SerializeField] private float scaleStart, scaleEnd;
    [SerializeField] private float minDistBetweenPlayer;
    [SerializeField] private GameObject nextTutButton;

    private bool isTarget1Done, isTarget2Done;
    private Transform player;
    
    private void Start()
    {
        nextTutButton.SetActive(false);
        nextTutButton.transform.localScale = new Vector3(0, 0, 0);
        
        GetComponent<ShootTutorialHandler>().enabled = false;
        
        moveToTargetText.SetActive(false);
        moveToTargetText.transform.localScale = new Vector3(0, 0, 0);
        
        player = FindObjectOfType<PlayerMovement>().transform;
        
        target1.SetActive(false);
        target2.SetActive(false);
        target1.transform.localScale = new Vector3(scaleStart, scaleStart, scaleStart);
        target2.transform.localScale = new Vector3(scaleStart, scaleStart, scaleStart);

        isTarget1Done = false;
        isTarget2Done = false;
        
        Invoke(nameof(MoveToText), 1f);
        Invoke(nameof(Target1), 1.5f);
    }

    private void Update()
    {
        if(!isTarget1Done)
            Target1Entered();
        
        if(!isTarget2Done)
            Target2Entered();
    }

    private void MoveToText()
    {
        moveToTargetText.SetActive(true);
        LeanTween.scale(moveToTargetText, new Vector3(1, 1, 1), 0.8f).setEaseOutBack();
    }
    
    private void Target1()
    {
        target1.SetActive(true);

        LeanTween.scale(target1, new Vector3(scaleEnd, scaleEnd, scaleEnd), 0.8f).setEaseOutBack();
        LeanTween.rotate(target1, new Vector3(0, 0, -180f), 0.8f).setEaseOutBack();

        LeanTween.value(target1, 0f, 1f, 0.8f).setOnUpdate((float val) =>
        {
            SpriteRenderer renderer = target1.GetComponent<SpriteRenderer>();
            Color c = renderer.color;
            c.a = val;
            renderer.color = c;
        });
    }
    
    private void Target2()
    {
        target2.SetActive(true);
        
        LeanTween.scale(target2, new Vector3(scaleEnd, scaleEnd, scaleEnd), 0.8f).setEaseOutBack();
        LeanTween.rotate(target2, new Vector3(0, 0, -180f), 0.8f).setEaseOutBack();

        LeanTween.value(target2, 0f, 1f, 0.8f).setOnUpdate((float val) =>
        {
            SpriteRenderer renderer = target2.GetComponent<SpriteRenderer>();
            Color c = renderer.color;
            c.a = val;
            renderer.color = c;
        });
    }

    private void Target1Entered()
    {
        if(Vector2.Distance(target1.transform.position, player.position) > minDistBetweenPlayer) return;
        
        LeanTween.scale(target1, new Vector3(0, 0, 0), 0.5f).setEaseInBack();
        StartCoroutine(DelayTarget(target1));

        isTarget1Done = true;
        
        Invoke(nameof(Target2), 1f);
    }
    
    private void Target2Entered()
    {
        if(Vector2.Distance(target2.transform.position, player.position) > minDistBetweenPlayer) return;
        
        LeanTween.scale(target2, new Vector3(0, 0, 0), 0.5f).setEaseInBack();
        StartCoroutine(DelayTarget(target2));
        
        isTarget2Done = true;

        StartCoroutine(MoveToTextDelay());
    }

    private IEnumerator DelayTarget(GameObject target)
    {
        yield return new WaitForSeconds(0.6f);
        target.SetActive(false);
    }

    private IEnumerator MoveToTextDelay()
    {
        LeanTween.scale(moveToTargetText, new Vector3(0, 0, 0), 0.8f).setEaseInBack();
        nextTutButton.SetActive(true);

        yield return new WaitForSeconds(0.9f);
        moveToTargetText.SetActive(false);
        
        nextTutButton.transform.GetChild(0).gameObject.SetActive(true);
        LeanTween.scale(nextTutButton, new Vector3(1, 1, 1), 0.6f).setEaseOutBack();
    }
}
