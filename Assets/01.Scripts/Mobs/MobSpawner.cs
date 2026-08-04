using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public MobPoolManager poolManager;
    public Transform playerTransform;
    private float spawnRadius = 10f;
    private float cooltime = 2f; //몹 소환 시간 간격
    private float lastSpawnTime = 0f; //마지막 소환 시간
    private float lessTime = 0.2f; //몹 소환 시간 간격 감소
    private float lastLessTime = 0f; //마지막 시간간격 감소 시간
    private float lessTimeCool = 20f; //시간 간격 감소 쿨타임

    //화면 밖 랜덤 위치 구하기
    Vector2 GetRendomPos()
    {
        Vector2 rendomDir = Random.insideUnitCircle.normalized;
        return (Vector2)playerTransform.position + (spawnRadius * rendomDir);
    }

    private void Update()
    {
        if (lastLessTime + lessTimeCool < Time.time && cooltime > 0.2f) //몹 소환 쿨타임 감소
        {
            lastLessTime = Time.time;
            cooltime -= lessTime;
            Debug.Log("쿨타임 감소");
        }
        if (lastSpawnTime + cooltime < Time.time)
        {
            SpawnRandomMob();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnRandomMob()
    {
        //랜덤 타입 선택
        int randomTypeIndex = Random.Range(0, System.Enum.GetValues(typeof(MobType)).Length);
        MobType seledtedType = (MobType)randomTypeIndex;
        Vector2 spawnPos;
        do
        {
            //위치 선정
            spawnPos = GetRendomPos();
        } while (PosCompare(spawnPos));

        //소환
        poolManager.SpawnMob(seledtedType, spawnPos);
    }

    //좌표가 맵 밖인지 확인
    private bool PosCompare(Vector2 spawnPos)
    {
        float leftEndPoint = -79.7f;
        float upEndPoint = 48.7f;
        float rightEndPoint = 81.8f;
        float downEndPoint = -47.49f;
        bool onMapOut = false;
        if (spawnPos.x < leftEndPoint || spawnPos.x > rightEndPoint) onMapOut = true;
        else if (spawnPos.y > upEndPoint || spawnPos.y < downEndPoint) onMapOut = true;
        //Debug.Log(onMapOut);
        return onMapOut;
    }
}
