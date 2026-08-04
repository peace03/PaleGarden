using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicCollision : MonoBehaviour
{
    public float eleShockDistanceTime = 0.5f;
    private float lastShockTime;
    private float damage;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        giveDamage(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(lastShockTime + eleShockDistanceTime < Time.time)
        {
            giveDamage(collision);
            lastShockTime = Time.time;
        }
    }
    private void giveDamage(Collider2D collision)
    {
        collision.gameObject.GetComponent<MobStat>().TakeDamage(damage);
        //Debug.Log($"{collision.name}의 체력: {collision.gameObject.GetComponent<MobStat>().GetHp()} - {damage}");
    }
}
