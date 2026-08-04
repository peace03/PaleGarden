using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public static GameManager Instance; //싱글톤
    public GameObject HP_Slider;
    public GameObject EXP_Slider;
    public GameObject Level_Text;
    public GameObject Stat_Overlay;
    public GameObject LevelUp_Panel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI survivalTimeText;

    private float timer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void GameStart()
    {
        HP_Slider.SetActive(true);
        EXP_Slider.SetActive(true);
        Level_Text.SetActive(true);
        Stat_Overlay.SetActive(true);
        LevelUp_Panel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    public void GameOver()
    {
        HP_Slider.SetActive(false);
        EXP_Slider.SetActive(false);
        Level_Text.SetActive(false);
        Stat_Overlay.SetActive(false);
        LevelUp_Panel.SetActive(false);
        int minute = Mathf.FloorToInt(timer / 60f);
        int second = Mathf.FloorToInt(timer - minute * 60);
        string survivalTime = $"{minute:00}:{second:00} survive";
        survivalTimeText.text = survivalTime;
        gameOverPanel.SetActive(true);
    }
}
