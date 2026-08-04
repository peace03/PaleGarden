using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Mob
{
    private Animator anim;
    private enum attackState { start, animation, end }
    private attackState curState = attackState.start;
    private float attackDuration = 1f;
    private float attackStartTime = 0f;
    private bool attackChance = true; //공격 모션동안 부딪히면 공격x
    //움직임
    public float moveSpeed = 1.5f;
    protected override float _moveSpeed => moveSpeed;
    //공격중 확인
    private bool attackExeNow = false;
    protected override bool inAttack => attackExeNow;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Attack()
    {
        switch (curState)
        {
            case attackState.start:
                attackStart();
                break;
            case attackState.animation:
                attackAnimation();
                break;
            case attackState.end:
                attackEnd();
                break;
            default:
                break;
        }
    }

    void attackStart()
    {
        attackStartTime = Time.time;
        attackExeNow = true;
        attackChance = true;
        anim.SetTrigger("DoAttack");
        curState = attackState.animation;
        //Debug.Log("공격 시작");
    }
    void attackAnimation()
    {
        if (attackStartTime + attackDuration < Time.time)
        {
            //애니메이션 재생
            //Debug.Log("공격 애니메이션");
            curState = attackState.end;
        }
    }
    void attackEnd()
    {
        attackExeNow = false;
        attackChance = false;
        curState = attackState.start;
        //Debug.Log("공격 끝");
    }
     
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attackChance == true && attackExeNow == true)
        {
            Debug.Log("Wolf의 공격");
            collision.gameObject.GetComponent<PlayerStat>().
                TakeDamage(GetComponent<MobStat>().Damage);
            attackChance = false;
        }
    }

    protected override Collider2D IsAttackRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 0.51f, playerLayer);
        return player;
    }
}
