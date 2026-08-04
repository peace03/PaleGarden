using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MobAttackPool : MonoBehaviour
{
    //몬스터 공격 오브젝트 풀, shooting Test 참고
    public GameObject prickleBulletPrefab;
    private IObjectPool<GameObject> _pricklePool;
    private int poolSize = 10;

    private void Awake()
    {
        _pricklePool = new ObjectPool<GameObject>(
           createFunc: CreatePrickle,
           actionOnGet: OnGetPrickle,
           actionOnRelease: OnReleasePrickle,
           actionOnDestroy: OnDestroyPrickle,
           collectionCheck: true,
           maxSize: poolSize
           );
    }

    private GameObject CreatePrickle()
    {
        GameObject prickleBullet = Instantiate(prickleBulletPrefab, transform);
        prickleBullet.GetComponent<PrickleBullet>().SetPool(_pricklePool);
        return prickleBullet;
    }
    private void OnGetPrickle(GameObject prickleBullet) => prickleBullet.SetActive(true);
    private void OnReleasePrickle(GameObject prickleBullet) => prickleBullet.SetActive(false);
    private void OnDestroyPrickle(GameObject prickleBullet) => Destroy(prickleBullet);
    public void Shoot(Vector2 startPos, GameObject target, float damage)
    {
        GameObject prickleBullet = _pricklePool.Get();
        prickleBullet.transform.position = startPos;
        prickleBullet.GetComponent<PrickleBullet>().SetBullet(target, damage);
    }
}
