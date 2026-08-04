using System; //Action 이벤트 사용
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Stat")]
    public float maxHp = 100f;
    public float curHp;
    public float curExp = 0;
    public float maxExp = 30;
    public int Level = 0;
    public float moveSpeed = 4.0f;
    [Header("Attack Power")]
    public float manaBulletNum = 1;
    public float manaBulletAtkPower = 10;
    //public float satelliteNum = 0;
    public float satelliteAtkPower = 10;
    public float electronicAtkPower = 5;
    public float FireAtkPower = 20;

    public event Action<float, float> OnHpChange;   //(현재체력, 최대체력)
    public event Action<float, float> OnExpChange;  //(현재경험치, 최대경험치)
    public event Action<int> OnLevelUp;             //현재 레벨
    public event Action IncreSateNum;               //위성체 생성
    public event Action<float> OnManaDamChange;     //마나탄 데미지 변경
    public event Action<float> OnSateDamChange;     //위성체 데미지 변경
    public event Action<float> OnElecDamChange;     //전기장 데미지 변경
    public event Action<float> OnFireDamChange;     //불 데미지 변경
    public event Action<float> OnMoveSpeedChange;   //moveSpeed 변경
    public event Action OnStatChange; //스탯 변경시 상태창 업데이트


    private bool isCreatedSate = false;
    private bool isCreatedElec = false;
    private bool isCreatedFire = false;

    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
        //초기상태 이벤트 발행
        OnHpChange?.Invoke(curHp, maxHp);
        OnExpChange?.Invoke(curExp, maxExp);
        //OnLevelUp?.Invoke(Level);
        //OnStatChange?.Invoke(); // 상태 이벤트 발행
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        OnHpChange?.Invoke(curHp, maxHp);
        //Debug.Log($"Player의 현재체력: {curHp}");
        if (curHp <= 0)
        {
            gameObject.SetActive(false); //죽으면
            Time.timeScale = 0f;
            GameManager.Instance.GameOver();
        }
    }
    public void TakeExp(float amount)
    {
        curExp += amount;
        LevelUp();
        //Debug.Log($"Level: {Level}\nExp: {curExp}\nmaxExp: {maxExp}");
    }
    private void LevelUp()
    {
        if(curExp > maxExp)
        {
            Level += 1;
            curExp = curExp - maxExp;
            maxExp *= 1.2f;
            OnLevelUp?.Invoke(Level);
        }
        OnExpChange?.Invoke(curExp, maxExp);
    }

    //아이템 효과 적용
    public void ApplyItem(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemData.ItemType.ManaAtkPow: //마나탄, 위성체 능력치 업그레이드 (없으면 생성)
                manaBulletAtkPower += item.value;
                if (!isCreatedSate)
                {
                    IncreSateNum?.Invoke();
                    isCreatedSate = true;
                }
                else satelliteAtkPower += item.value;
                OnManaDamChange?.Invoke(manaBulletAtkPower);
                OnSateDamChange?.Invoke(satelliteAtkPower);
                break;
            case ItemData.ItemType.ManaNum: //마나탄 개수 증가
                manaBulletNum += item.value;
                break;
            case ItemData.ItemType.ElecAtkPow: //전기장 공격력 증가
                if (!isCreatedElec)
                {
                    GetComponent<Electronic>().AddElectronic();
                    isCreatedElec = true;
                }
                else
                {
                    electronicAtkPower += item.value;
                    OnElecDamChange?.Invoke(electronicAtkPower);
                }
                break;
            case ItemData.ItemType.FireAtkPow: //불 공격력 증가
                if (!isCreatedFire)
                {
                    GetComponent<Fire>().AddFire();
                    isCreatedFire = true;
                }
                else
                {
                    FireAtkPower += item.value;
                    OnFireDamChange?.Invoke(FireAtkPower);
                }
                break;
            case ItemData.ItemType.SateNum: //위성체 개수 증가
                IncreSateNum?.Invoke();
                isCreatedSate = true;
                break;
            case ItemData.ItemType.MaxHp: //최대 체력 증가
                maxHp += item.value;
                curHp += item.value;
                OnHpChange?.Invoke(curHp, maxHp);
                break;
            case ItemData.ItemType.Heal: //체력 회복
                curHp += item.value;
                if (curHp > maxHp) curHp = maxHp;
                OnHpChange?.Invoke(curHp, maxHp);
                break;
            case ItemData.ItemType.Speed: //이동속도 증가
                moveSpeed += item.value;
                OnMoveSpeedChange?.Invoke(moveSpeed);
                break;
            default:
                break;
        }
        OnStatChange?.Invoke(); //카드 선택마다 상태창 업데이트
    }
}
