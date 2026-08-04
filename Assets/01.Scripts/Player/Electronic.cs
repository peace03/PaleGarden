using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronic : MonoBehaviour
{
    public GameObject electronicPrefab;
    private GameObject elecObj;

    private void Start()
    {
        GetComponent<PlayerStat>().OnElecDamChange += DamageUpdate;
    }

    //카드 선택시 데미지 업데이트
    void DamageUpdate(float amount)
    {
        elecObj.GetComponent<ElectronicCollision>().SetDamage(amount);
    }

    public void AddElectronic()
    {
        elecObj = Instantiate(electronicPrefab, transform.position, Quaternion.identity, transform);
        //electronic 오브젝트 damage 설정
        elecObj.GetComponent<ElectronicCollision>().
            SetDamage(GetComponent<PlayerStat>().electronicAtkPower);
    }


}
