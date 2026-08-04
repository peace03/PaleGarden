using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExpPoolManager : MonoBehaviour
{
    private IObjectPool<GameObject> _pool;
    public GameObject ExpPrefab;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateExp,
            actionOnGet: exp => exp.SetActive(true),
            actionOnRelease: exp => exp.SetActive(false),
            actionOnDestroy: exp => Destroy(exp),
            collectionCheck: true,
            maxSize: 20
            );
    }
    private GameObject CreateExp()
    {
        GameObject Exp = Instantiate(ExpPrefab, transform);
        Exp.GetComponent<EXP>().SetPool(_pool);
        return Exp;
    }
    //MobStat의 OnDeath 이벤트에 등록됨
    public void DropExp(Vector2 spawnPos) //매개변수 일치하는지 확인
    {
        GameObject exp = _pool.Get();
        exp.transform.position = spawnPos;
    }
}
