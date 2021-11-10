using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject SplashPanel, LevelPassPanel, LevelFailPanel;
    public Text moneyText, levelText;

    public void Splash()
    {
        SplashPanel.SetActive(true);
        LevelPassPanel.SetActive(false);
        LevelFailPanel.SetActive(false);
    }

    public void InGame()
    {
        SplashPanel.SetActive(false);
        LevelPassPanel.SetActive(false);
        LevelFailPanel.SetActive(false);
    }
    public void LevelPass()
    {
        SplashPanel.SetActive(false);
        LevelPassPanel.SetActive(true);
        LevelFailPanel.SetActive(false);
    }
    
    public void LevelFail()
    {
        SplashPanel.SetActive(false);
        LevelPassPanel.SetActive(false);
        LevelFailPanel.SetActive(true);
    }

    public void UpdateLevelText(int value)
    {
        levelText.text = "Level: " + value.ToString();
    }

    public void UpdateMoneyText(int value)
    {
        moneyText.text = value.ToString();
    }
}
