using System;
using UnityEngine;
using UnityEngine.UI;

public class BossesWinLose : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    private bool hasWon;
    private bool hasLost;
    private bool hasBossSpawned;

    private void Start()
    {
        menu.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.transform.localScale = new Vector3(0, 0, 0);
        loseScreen.transform.localScale = new Vector3(0, 0, 0);
        hasWon = false;
        hasLost = false;
        
        Invoke(nameof(BossSpawned), 3f);
    }

    private void Update()
    {
        if (hasBossSpawned)
            Check();
    }

    private void Check()
    {
        if (GameObject.FindGameObjectsWithTag("Boss").Length == 0 && !hasWon && GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
        {
            hasWon = true;
            WinMenu();
        }

        if (GameObject.FindGameObjectsWithTag("Player").Length == 0 && !hasLost)
        {
            hasLost = true;
            LoseMenu();
        }
    }

    private void BossSpawned()
    {
        hasBossSpawned = true;
    }

    private void WinMenu()
    {
        menu.SetActive(true);
        
        LeanTween.value(menu, 0f, 0.65f, 0.4f).setOnUpdate((float val) =>
        {
            Image r = menu.GetComponent<Image>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        
        winScreen.SetActive(true);
        LeanTween.scale(winScreen, new Vector3(1, 1, 1), 0.6f);
    }
    
    private void LoseMenu()
    {
        menu.SetActive(true);
        
        LeanTween.value(menu, 0f, 0.65f, 0.4f).setOnUpdate((float val) =>
        {
            Image r = menu.GetComponent<Image>();
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        
        loseScreen.SetActive(true);
        LeanTween.scale(loseScreen, new Vector3(1, 1, 1), 0.6f);
    }
}
