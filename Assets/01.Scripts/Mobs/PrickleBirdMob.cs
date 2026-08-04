using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrickleBirdMob : Mob
{
    private GameObject MobBulletManager;
    public float detectRange = 3f;
    private float attackCoolTime = 1.5f;
    private float lastAttackTime = 0f;
    //框流烙
    public float moveSpeed = 1.5f;
    protected override float _moveSpeed => moveSpeed;

    private void Start()
    {
        MobBulletManager = GameObject.Find("PrickleBulletPool");
    }

    protected override void Attack()
    {
        if (lastAttackTime+attackCoolTime < Time.time)
        {
            //阂房 积己
            MobBulletManager.GetComponent<MobAttackPool>().
                Shoot(transform.position, Player, gameObject.GetComponent<MobStat>().Damage);
            lastAttackTime = Time.time;
        }
    }

    protected override Collider2D IsAttackRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange, playerLayer);
        return player;
    }


}
