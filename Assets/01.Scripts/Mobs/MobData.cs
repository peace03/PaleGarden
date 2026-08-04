using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobData", menuName = "Scriptable Object/Mob Data")]
public class MobData : ScriptableObject
{
    public string mobName;
    public float maxHp;
    public float moveSpeed;
    public float damage;
}
