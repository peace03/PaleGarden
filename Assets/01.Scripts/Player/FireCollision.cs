using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    private float damage;
    private float lastAtkTime = 0f;
    private float atkDistanceTime = 0.5f;

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
        if (lastAtkTime + atkDistanceTime < Time.time)
        {
            giveDamage(collision);
            lastAtkTime = Time.time;
        }
    }
    private void giveDamage(Collider2D collision)
    {
        collision.gameObject.GetComponent<MobStat>().TakeDamage(damage);
        //Debug.Log($"{collision.name}의 체력: {collision.gameObject.GetComponent<MobStat>().GetHp()} - {damage}");
    }
}
