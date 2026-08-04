using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//몬스터 종류
public enum MobType
{
    Rush,
    PrickleBird,
    Wolf
}

public class MobPoolManager : MonoBehaviour
{
    //인스펙터에서 데이터 구조체 설정
    [System.Serializable]
    public struct PoolInfo
    {
        public MobType type;
        public GameObject prefab;
        public int size;
    }

    [Header("Settings")]
    public List<PoolInfo> mobPools; //3종류 몬스터 등록
    [Header("Dependencies")]
    public ExpPoolManager expManager;

    //딕셔너리로 풀 관리
    private Dictionary<MobType, IObjectPool<GameObject>> _pools;

    private void Awake()
    {
        _pools = new Dictionary<MobType, IObjectPool<GameObject>>();
        //풀 생성 및 등록
        foreach(var info in mobPools)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => CreateMob(info.prefab),
                actionOnGet: mob => mob.SetActive(true),
                actionOnRelease: mob => mob.SetActive(false),
                actionOnDestroy: mob => Destroy(mob),
                maxSize: info.size
                );
            _pools.Add(info.type, pool);
        }
    }

    //몹 생성
    private GameObject CreateMob(GameObject prefab)
    {
        GameObject mob = Instantiate(prefab, transform);
        //몬스터가 태어날 때, 경험치 매니저를 구독자로 등록
        //해당 mob이 죽으면(OnDeath), 경험치 매니저의 DropExp 함수를 실행해라.
        mob.GetComponent<MobStat>().OnDeath += expManager.DropExp;
        return mob;
    }

    //외부에서 몹 요청
    public GameObject SpawnMob(MobType type, Vector2 position)
    {
        if (_pools.ContainsKey(type))
        {
            GameObject mob = _pools[type].Get();
            mob.transform.position = position;
            mob.GetComponent<MobStat>().SetPool(_pools[type]);
            return mob;
        }
        else
        {
            Debug.LogError($"풀에 {type}이 등록되지 않았습니다.");
            return null;
        }
    }
}
