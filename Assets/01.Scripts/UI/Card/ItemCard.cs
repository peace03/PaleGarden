using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //Action 사용

public class ItemCard : MonoBehaviour
{
    [Header("UI Elements")]
    public Image iconImg;
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descTxt;
    public Button cardBtn; //클릭할 버튼

    //데이터와 클릭했을 때 실행할 함수 받아옴
    public void Setup(ItemData data, Action<ItemData> onClick)
    {
        //1. UI 채우기
        iconImg.sprite = data.itemIcon;
        nameTxt.text = data.itemName;
        descTxt.text = data.itemDesc;

        //2. 버튼 클릭 이벤트 연결
        //기존 연결된게 있다면 제거 후 다시연결
        cardBtn.onClick.RemoveAllListeners();
        cardBtn.onClick.AddListener(() => onClick(data));
    }
}
