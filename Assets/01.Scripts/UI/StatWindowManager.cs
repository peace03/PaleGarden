using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatWindowManager : MonoBehaviour
{
    public PlayerStat playerStat;
    public GameObject statPanel; //껐다 킬 패널 (Stat_Overlay)
    public Transform contentArea; //텍스트가 생길 부모 위치(Stat_Overlay 위치)
    public GameObject textPrefab; //Stat_Row_Text 프리팹

    private bool isOpen = false;
    private List<GameObject> rowPool = new List<GameObject>(); //생성된 텍스트 관리

    // Start is called before the first frame update
    void Start()
    {
        playerStat.OnStatChange += UpdateStatWindow;
        statPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            statPanel.SetActive(isOpen);
            if (isOpen) UpdateStatWindow(); //켜질때 정보갱신
        }
    }

    void UpdateStatWindow()
    {
        //기존 텍스트 꺼두기
        foreach (var row in rowPool)
        {
            row.SetActive(false);
        }
        //순서대로 채워넣기
        int index = 0;
        SetRow(index++, $"MAX_HP : {playerStat.maxHp}");
        // 집에서 추가 작성
        SetRow(index++, $"Max_EXP : {playerStat.maxExp:F0}");
        SetRow(index++, $"Move Speed : {playerStat.moveSpeed}");
        SetRow(index++, $"Mana AtkPower : {playerStat.manaBulletAtkPower}");
        SetRow(index++, $"Satellite AtkPower : {playerStat.satelliteAtkPower}");
        SetRow(index++, $"Electronic AtkPower : {playerStat.electronicAtkPower}");
        SetRow(index++, $"Fire AtkPower : {playerStat.FireAtkPower}");
        if (isOpen)
        {
            foreach (var row in rowPool)
            {
                row.SetActive(true);
            }
        }
    }
    void SetRow(int index, string content)
    {
        GameObject row;
        //List에 쓸 수 있는 오브젝트가 이미 있다면 재사용
        if (index < rowPool.Count)
        {
            row = rowPool[index];
            row.SetActive(true);
        }
        else //모자라면 새로만들기
        {
            row = Instantiate(textPrefab, contentArea);
            rowPool.Add(row);
        }
        row.GetComponent<TextMeshProUGUI>().text = content;
    }
}
