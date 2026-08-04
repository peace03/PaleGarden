using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<GameObject> _managedPool;
    private GameObject target;
    public float bulletSpeed = 10f; //√—æÀ º”µµ
    private float damage;
    private bool isReleased = false;
    //¿«¡∏º∫ ¡÷¿‘
    public void SetManagedPool(IObjectPool<GameObject> pool) => _managedPool = pool;
    //√—æÀ π›≥≥
    public void ReturnBullet()
    {
        if (isReleased == false)
        {
            _managedPool.Release(this.gameObject);
            isReleased = true;
        }
    }
    void Update()
    {
        Move();
    }

    public void SetTarget(GameObject target, float damage)
    {
        this.target = target;
        isReleased = false;
        this.damage = damage;
    }

    void Move()
    {
        if (target == null || !target.activeSelf)
        {
            ReturnBullet();
            return;
        }
        Vector2 direction = (target.transform.position - this.transform.position).normalized;
        transform.right = direction;
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //√Êµπµ«∏È bullet ªÁ∂Û¡¸
        ReturnBullet();
        collision.GetComponent<MobStat>().TakeDamage(damage);
    }
}
