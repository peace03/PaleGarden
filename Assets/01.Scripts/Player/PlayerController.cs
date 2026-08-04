using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerAttackPool; //bullet 스폰
    private Rigidbody2D rb;
    //private Animator anim;
    //private SpriteRenderer spriteRenderer;
    private float moveSpeed; //움직이는 속도
    private float shootTime = 0f;
    public float manaBulletCoolTime = 5f;
    public float radius = 5f; //OverlapCircleAll 반지름
    public LayerMask MobLayer; //OverlapCircleAll 감지 레이어
    private GameObject target = null;

    private float delayTime; //총 1회 발사간 딜레이

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = GetComponent<PlayerStat>().moveSpeed;
        GetComponent<PlayerStat>().OnMoveSpeedChange += MoveSpeedUpdate;
        //anim = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
        CloseTargetting();
    }

    void Update()
    {
        
        //쿨타임 차고 타겟 나타나면 공격
        if (Time.time > shootTime + manaBulletCoolTime && target != null)
        {
            StartCoroutine(ShootRoutine(target)); //코루틴 사용
            shootTime = Time.time;
            target = null;
        }
    }

    IEnumerator ShootRoutine(GameObject target)
    {
        for (int i = 0; i < (int)GetComponent<PlayerStat>().manaBulletNum; i++) //마나탄 1회 발사 개수만큼 생성
        {
            PlayerAttackPool.GetComponent<PlayerAttackPool>().
                Shoot(transform.position, target, gameObject.GetComponent<PlayerStat>().manaBulletAtkPower);
            delayTime = Time.time;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * moveSpeed;
    }

    void MoveSpeedUpdate(float amount)
    {
        moveSpeed = GetComponent<PlayerStat>().moveSpeed;
        Debug.Log($"moveSpeed: {amount}");
    }

    void CloseTargetting()
    {
        //가장 가까운 적 타겟팅 -> 여유되면 OverlapCircleNonAlloc
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, MobLayer);
        if (targets.Length == 0) return;
        Collider2D closeTarget = null;
        float distance = 30f;
        foreach (Collider2D target in targets)
        {
            float temp = Vector2.Distance(transform.position, target.transform.position);
            if (distance > temp)
            { 
                distance = temp;
                closeTarget = target;
            }
        }
        target = closeTarget.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }
}
