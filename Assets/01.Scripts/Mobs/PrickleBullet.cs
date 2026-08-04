using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PrickleBullet : MonoBehaviour
{
    //불렛 이동
    Vector2 dir;
    public float bulletSpeed = 5;
    private float destroyTime = 5f;
    private float creBulletTime;
    private bool isReleased = false; //반납되었는지 확인
    private float damage;

    //오브젝트 풀 의존성 주입
    private IObjectPool<GameObject> _pool;
    public void SetPool(IObjectPool<GameObject> pool) => _pool = pool;
    public void Release()
    {
        if (isReleased == false) //두번 반납 방지
        {
            _pool.Release(gameObject);
            isReleased = true;
        }
        
    }
    private void OnEnable()
    {
        isReleased = false;
    }

    void Update()
    {
        if (creBulletTime + destroyTime < Time.time) Release();
        //이동
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Release();
        collision.GetComponent<PlayerStat>().TakeDamage(damage);
    }

    public void SetBullet(GameObject target, float damage)
    {
        //활성화 시간
        creBulletTime = Time.time;
        //발사 방향 정해주기
        dir = (target.transform.position - transform.position).normalized;
        transform.up = dir;
        this.damage = damage;
    }
}
