using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; //TextMeshPro 사용

public class HUDController : MonoBehaviour
{
    [Header("UI connector")]
    public PlayerStat playerStat; //플레이어 스탯 변화 발행자
    public Slider hpSlider; //체력바
    public Slider expSlider; //경험치바
    public TextMeshProUGUI levelText; //레벨텍스트

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 발행자 구독
        playerStat.OnHpChange += UpdateHpBar;
        playerStat.OnExpChange += UpdateExpBar;
        playerStat.OnLevelUp += UpdateLevelText;
        GameManager.Instance.GameStart();
    }

    void UpdateHpBar(float cur, float max) => hpSlider.value = cur / max;
    void UpdateExpBar(float cur, float max) => expSlider.value = cur / max;
    void UpdateLevelText(int level) => levelText.text = $"LV.{level}";
}
