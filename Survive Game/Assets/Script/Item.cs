using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] //�����Ҷ� ������Ʈ�� �ϳ� ���� ����
public class Item : ScriptableObject // ��뿡�̺��� �ƴ� ��ũ��Ƽ�� ������Ʈ (������Ʈ�� ���� ���� �ʿ䰡 ����)
{
    public string itemName;  //������ �̸�
    public ItemType itemType; //�������� ����
    public Sprite itemImage; //������ �̹���
    public GameObject itemPrefab; //�������� ������ (�������� �������� �� ��ü)
    
    
    public string weaponType; // ���� ����
    public enum ItemType //������
    { 
        Equitment, //���
        Used, //�Ҹ�
        Ingredient, // ���
        ETC //��Ÿ

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
