using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float range; //���� ������ �Ÿ�
    private bool pickupActivated = false; //���� ������ �� true

    private RaycastHit hitinfo; //�浹ü ���� ����.

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    private LayerMask layerMask; // ������ ���̾ �ٶ󺸰� �־�߸� ȹ�� �����ϰ� ����


    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;
    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitinfo.transform != null) // Ȥ�ø� ���� ������ �줿��,,
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ�. ");
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitinfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }
        private void CheckItem()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
            {
                if (hitinfo.transform.tag == "Item")
                {
                    ItemInfoAppear();
                }
            }
            else
            {
                InfoDisappear();
            }
        }
        private void ItemInfoAppear()
        {
            pickupActivated = true;
            actionText.gameObject.SetActive(true);
            actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
        }
        private void InfoDisappear()
        {
            pickupActivated = false; //�ֿ� �� ����
            actionText.gameObject.SetActive(false);  //������� ����(�ؽ�Ʈ��)
        }
    } 
