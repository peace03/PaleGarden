using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerAttackPool : MonoBehaviour
{
    //플레이어용 오브젝트 풀
    [Header("Settings")]
    public GameObject BulletPrefab;
    public int maxCapacity = 20; //최대 개수

    //풀 객체
    private IObjectPool<GameObject> _pool;
   

    private void Awake()
    {
        //풀 초기화
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,           //생성규칙
            actionOnGet: OnGetBullet,           //대여규칙
            actionOnRelease: OnReleaseBullet,   //반납규칙
            actionOnDestroy: OnDestoryBullet,   //파괴규칙
            collectionCheck: true,              //동일한것 두번 반납 방지
            maxSize: maxCapacity                //풀 사이즈
            );
    }   
    
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab, transform);
        bullet.GetComponent<Bullet>().SetManagedPool(_pool); //의존성 주입
        return bullet;
    }
    private void OnGetBullet(GameObject bullet) => bullet.SetActive(true);
    private void OnReleaseBullet(GameObject bullet) => bullet.SetActive(false);
    private void OnDestoryBullet(GameObject bullet) => Destroy(bullet);
    public void Shoot(Vector2 position, GameObject target, float damage)
    {
        GameObject bullet = _pool.Get();
        bullet.transform.position = position;
        bullet.GetComponent<Bullet>().SetTarget(target, damage);
        //Debug.Log($"마나탄의 피격 데미지: {damage}");
    }
}
