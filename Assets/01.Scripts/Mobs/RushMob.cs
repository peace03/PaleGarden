using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushMob : Mob
{
    Vector2 Attackdir; //공격 방향
    private enum State { start, during, coolDown } //공격상태
    private State curState = State.start;
    public float attackSpeed = 5f;
    public float detectRange = 3f;
    public float attackDuration = 1f; //공격 지속시간
    private float StateEndTime = 0f; //공격 끝날 시간
    public float coolTime = 0.5f; //공격 쿨타임 시간
    //움직임
    public float moveSpeed = 1.5f;
    protected override float _moveSpeed => moveSpeed;

    private bool attackChance = true; //attack 메소드당 1회 공격
    private bool attackExeNow = false; //어택 실행 여부
    protected override bool inAttack => attackExeNow;

    protected override Collider2D IsAttackRange()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange, playerLayer);
        return player;
    }
    protected override void Attack()
    {
        switch (curState)
        {
            case State.start:
                AttackStart();
                break;
            case State.during:
                DuringAttack();
                break;
            case State.coolDown:
                CoolDown();
                break;
            default:
                break;
        }
    }

    void AttackStart()
    {
        attackChance = true;
        attackExeNow = true;
        StateEndTime = Time.time + attackDuration;
        Attackdir = (Player.transform.position - transform.position).normalized;
        curState = State.during;
        //Debug.Log("공격시작");
    }
    void DuringAttack()
    {
        if (Time.time < StateEndTime)
        {
            transform.Translate(Attackdir * attackSpeed * Time.deltaTime);
        }
        else
        {
            StateEndTime += coolTime;
            attackChance = false;
            curState = State.coolDown;
        }
    }
    void CoolDown()
    {
        if (StateEndTime < Time.time)
        {
            curState = State.start;
            attackExeNow = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attackChance == true && attackExeNow == true)
        {
            PlayerStat script = collision.gameObject.GetComponent<PlayerStat>();
            if (script != null) script.TakeDamage(GetComponent<MobStat>().Damage);
            attackChance = false;
        }
    }
}
