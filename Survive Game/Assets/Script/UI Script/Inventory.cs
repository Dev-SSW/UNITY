using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    
    //�ʿ��� ������Ʈ
    [SerializeField]
    private GameObject go_InventoryBase; //inventorybase
    [SerializeField]
    private GameObject go_SlotsParents; //gridsetting

    private Slot[] slots; //���Ե�

    public Slot[] GetSlots() { return slots; }

    [SerializeField] private Item[] items;
    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
                slots[_arrayNum].Additem(items[i], _itemNum);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParents.GetComponentsInChildren<Slot>();    
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }
    private void OpenInventory()
    {
        GameManager.isOpenInventory = true;
        go_InventoryBase.SetActive(true);
    }
    private void CloseInventory()
    {
        GameManager.isOpenInventory = false;
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equitment != _item.itemType)  //��� �ƴҶ���
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                {
                slots[i].Additem(_item, _count);
                return;
                }
        }
    }
}
