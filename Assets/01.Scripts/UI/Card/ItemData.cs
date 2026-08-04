using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public Sprite itemIcon;
    [TextArea] public string itemDesc; //설명

    public enum ItemType
    {
        ManaAtkPow, ManaNum, ElecAtkPow, FireAtkPow, SateNum, MaxHp, Heal, Speed
    }
    [Header("Stat")]
    //어떤 능력치를 올릴지 타입 결정
    public ItemType itemType;

    public float value;
}
