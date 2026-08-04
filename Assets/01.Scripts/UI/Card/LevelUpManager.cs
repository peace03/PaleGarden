using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [Header("References")]
    public PlayerStat playerStat; //플레이어
    public GameObject levelUpPanel; //레벨업 UI 패널
    public Transform cardsParent; //카드가 생성될 위치 (Layout Group)
    public GameObject cardPrefab; //카드 프리펩

    [Header("Data")]
    public List<ItemData> allItems; //전체 아이템 리스트

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 레벨업 이벤트 구독
        playerStat.OnLevelUp += ShowLevelUpWindow;
        levelUpPanel.SetActive(false);
    }
    void ShowLevelUpWindow(int level)
    {
        //게임 일시정지
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);

        //기존 카드 청소
        foreach (Transform child in cardsParent) Destroy(child.gameObject);

        //랜덤 아이템 3개 뽑기
        int[] randIndices = GetRandomIndices(3, allItems.Count);

        for (int i = 0; i < randIndices.Length; i++)
        {
            ItemData data = allItems[randIndices[i]];
            //카드생성
            GameObject newCard = Instantiate(cardPrefab, cardsParent);
            //카드 설정
            newCard.GetComponent<ItemCard>().Setup(data, OnItemSelect);
        }
    }

    void OnItemSelect(ItemData selectedItem)
    {
        Debug.Log($"선택한 아이템: {selectedItem.itemName}");
        //플레이어 능력치 적용
        playerStat.ApplyItem(selectedItem);
        //창 닫고 게임 재개
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    int[] GetRandomIndices(int count, int max)
    {
        List<int> candidates = new List<int>();
        for (int i = 0; i < max; i++) candidates.Add(i);

        int[] results = new int[Mathf.Min(count, max)];
        for (int i = 0; i < results.Length; i++)
        {
            int randomIndex = Random.Range(0, candidates.Count);
            results[i] = candidates[randomIndex];
            candidates.RemoveAt(randomIndex);
        }
        return results.ToArray();
    }
}
