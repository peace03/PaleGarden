using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    protected GameObject Player;
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    private Collider2D player;
    private SpriteRenderer _spriteRenderer;
    protected virtual float _moveSpeed => 1.5f;
    protected virtual bool inAttack => false; //attack 메서드가 실행되어야하는지

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        player = IsAttackRange();
        if (Player != null)
        {
            if (player == null && inAttack == false)
            {
                Vector2 dir = (Player.transform.position - transform.position).normalized;
                rb.velocity = dir * _moveSpeed;
                if (dir.x < 0) _spriteRenderer.flipX = false;
                else _spriteRenderer.flipX = true;
            }
            else rb.velocity = Vector2.zero;
        }
    }

    // 부모 클래스만 update사용
    private void Update()
    {
        if (player != null || inAttack == true) //플레이어가 공격범위 안 || 공격루프
        {
            Attack();
        }
    }

    protected abstract Collider2D IsAttackRange(); //공격범위 내에 있는지
    protected abstract void Attack(); //공격
}
