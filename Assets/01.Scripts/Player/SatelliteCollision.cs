using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollision : MonoBehaviour
{
    private float damage;

    private void Update()
    {
        
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<MobStat>().TakeDamage(damage);
        //Debug.Log($"{collision.name}ÀÇ Ã¼·Â: {collision.gameObject.GetComponent<MobStat>().GetHp()}");
    }
}
