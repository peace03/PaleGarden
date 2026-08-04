using System; //Action 사용
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class MobStat : MonoBehaviour
{
    [SerializeField] private MobData _data;
    private float _curHp;
    public event Action<Vector2> OnDeath;

    private IObjectPool<GameObject> _poolManager;
    private bool isReleased = false;

    public float MoveSpeed => _data.moveSpeed;
    public float Damage => _data.damage;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        _poolManager = pool;
    }

    private void OnEnable()
    {
        if (_data != null)
        {
            _curHp = _data.maxHp;
            isReleased = false;
        }
    }

    public void TakeDamage(float amount)
    {
        _curHp -= amount;
        //Debug.Log($"{gameObject.name}현재 체력: {_curHp}");
        if(_curHp <= 0 && isReleased == false)
        {
            OnDeath?.Invoke(transform.position); //구독자가 있다면, 내 위치를 알려줘라
            _poolManager.Release(gameObject);
            isReleased = true;
        }
        Debug.Log($"{amount} 피격");
    }

    public float GetHp()
    {
        return _curHp;
    }
}
