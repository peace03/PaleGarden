using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    [Header("Settings")]
    public GameObject satellitePrefab; //위성 프리펩
    public float radius = 3.0f; //플레어로부터 거리
    public float rotationSpeed = 100f; //공전속도 (도/초)

    private List<GameObject> _satellites = new List<GameObject>();
    private float _currentAngle = 0f; //기준각도
    private void Start()
    {
        GetComponent<PlayerStat>().OnSateDamChange += DamageUpdate;
        GetComponent<PlayerStat>().IncreSateNum += AddSatellite; //위성체 생성 구독
    }

    private void FixedUpdate()
    {
        //전체 회전
        _currentAngle += rotationSpeed * Time.deltaTime;
        //각도 초기화
        if (_currentAngle >= 360f) _currentAngle -= 360f;
        //위성 위치 재배치
        UpdateSatellitePositions();
    }

    void DamageUpdate(float damage)
    {
        foreach (var sate in _satellites)
        {
            sate.GetComponent<SatelliteCollision>().SetDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //[테스트용] spacebar누르면 위성 추가
        if (Input.GetKeyDown(KeyCode.Space)) AddSatellite();
    }
    public void AddSatellite()
    {
        //위성 생성
        GameObject newSat = Instantiate(satellitePrefab, transform.position, Quaternion.identity, transform);
        newSat.gameObject.GetComponent<SatelliteCollision>().SetDamage(GetComponent<PlayerStat>().satelliteAtkPower);
        _satellites.Add(newSat);
    }
    private void UpdateSatellitePositions()
    {
        if (_satellites.Count == 0) return;
        //360도 n개로 쪼개기
        float angleStep = 360f / _satellites.Count;
        for (int i = 0; i < _satellites.Count; i++)
        {
            //각 위성의 최종각도 = (현재 회전각) + (간격 * 내 순서)
            float targetAngle = _currentAngle + (angleStep * i);
            //삼각함수 cos, sin은 radian값을 받으므로 변환한다
            float radian = targetAngle * Mathf.Deg2Rad;
            //위치계산: cos * r, sin * r
            Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * radius;
            //최종 위치 적용 (내 위치 + 오프셋)
            _satellites[i].transform.position = transform.position + offset;
        }
    }   
}
