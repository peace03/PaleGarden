using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EXP : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    private float expAmount = 5f;
    private bool isReleased = false;
    private void OnEnable()
    {
        isReleased = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerStat>().TakeExp(expAmount);
        pool.Release(gameObject);
        isReleased = true;
    }

    public void SetPool(IObjectPool<GameObject> _pool) => pool = _pool;
}