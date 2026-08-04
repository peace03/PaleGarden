using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject FirePrefab;
    private GameObject FireObj; //인스턴스화 된 Fire
    public Transform FirePivot;
    private float rotationSpeed = 10f;
    private bool isCreated = false;
    private void Awake()
    {
        FirePrefab.GetComponent<FireCollision>().SetDamage(GetComponent<PlayerStat>().FireAtkPower);
        GetComponent<PlayerStat>().OnFireDamChange += DamageUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(h, v);

        //입력이 있을때만 회전 (키를 떼면 마지막 방향 유지)
        if (dir.sqrMagnitude > 0.01f && isCreated == true)
        {
            //LookRotation은 3D벡터(z축)을 건드려서 2D 스프라이트가 안보일수도있다.
            //직접 계산
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //z축을 기준으로하는 쿼터니언 생성
            Quaternion targetRot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            //FirePivot 회전
            FirePivot.rotation = Quaternion.Slerp(FirePivot.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    //카드 선택시 불 데미지 업데이트
    void DamageUpdate(float amount)
    {
        FireObj.GetComponent<FireCollision>().SetDamage(amount);
    }

    public void AddFire()
    {
        isCreated = true;
        Vector3 pos = FirePivot.position + (FirePivot.up * 0.9f);
        FireObj = Instantiate(FirePrefab, pos, Quaternion.identity, FirePivot);
        FireObj.GetComponent<FireCollision>().SetDamage(GetComponent<PlayerStat>().FireAtkPower);
    }
}
