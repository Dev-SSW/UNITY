using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string CloseWeaponName; // 근접 무기 이름

    //웨폰 유형
    public bool isHand;
    public bool isAxe;
    public bool isPickAxe;

    public float range; // 공격범위
    public int damage; // 공격력
    public float workSpeed; // 작업 속도
    public float attackDelay; // 공격 딜레이
    public float attackDelayA; // 공격 활성화 시점 (닿을 때 때리고)
    public float attackDelayB; // 공격 비활성화 시점 ( 뺼 때 안 때리게)

    public Animator anim; // 애니메이션
}
