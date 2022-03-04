using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ShootTutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject enemy1, enemy2;
    [SerializeField] private Transform spawnLoc1, spawnLoc2;
    [SerializeField] private GameObject circleIndicator;
    [SerializeField] private GameObject shootEnemiesText;
    [SerializeField] private GameObject nextTutorialButton;
    private bool isEnemy1Dead;

    private void Start()
    {
        shootEnemiesText.SetActive(false);
        shootEnemiesText.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(SpawnEnemy1());
    }

    private IEnumerator SpawnEnemy1()
    {
        yield return new WaitForSeconds(0.6f);
        shootEnemiesText.SetActive(true);
        LeanTween.scale(shootEnemiesText, new Vector3(1, 1, 1), 0.8f).setEaseOutBack();

        Instantiate(circleIndicator, spawnLoc1.position, quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(enemy1, spawnLoc1.position, quaternion.identity);
    }

    public void SpawnSecondEnemy()
    {
        StartCoroutine(SpawnEnemy2());
    }

    private IEnumerator SpawnEnemy2()
    {
        Instantiate(circleIndicator, spawnLoc2.position, quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(enemy2, spawnLoc2.position, quaternion.identity);
    }

    public void StartNextTut()
    {
        LeanTween.scale(shootEnemiesText, new Vector3(0, 0, 0), 0.8f).setEaseInBack();
        
        nextTutorialButton.SetActive(true);
        nextTutorialButton.transform.GetChild(0).gameObject.SetActive(false);
        nextTutorialButton.transform.GetChild(1).gameObject.SetActive(true);
        LeanTween.scale(nextTutorialButton, new Vector3(1, 1, 1), 0.5f).setEaseOutBack();
    }

    public void NextTutButtonHide()
    {
        LeanTween.scale(nextTutorialButton, new Vector3(0, 0, 0), 0.5f).setEaseInBack();
        StartCoroutine(TutButtonHide());
    }

    private IEnumerator TutButtonHide()
    {
        yield return new WaitForSeconds(0.6f);
        nextTutorialButton.SetActive(false);
        nextTutorialButton.transform.GetChild(0).gameObject.SetActive(false);
        nextTutorialButton.transform.GetChild(1).gameObject.SetActive(true);
    }
}