using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] //생성할때 오브젝트를 하나 따로 만들어냄
public class Item : ScriptableObject // 모노에이블이 아닌 스크립티블 오브젝트 (오브젝트에 따로 붙일 필요가 없음)
{
    public string itemName;  //아이템 이름
    public ItemType itemType; //아이템의 유형
    public Sprite itemImage; //아이템 이미지
    public GameObject itemPrefab; //아이템의 프리팹 (아이템이 떨궈졌을 때 실체)
    
    
    public string weaponType; // 무기 유형
    public enum ItemType //열거형
    { 
        Equitment, //장비
        Used, //소모성
        Ingredient, // 재료
        ETC //기타

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
