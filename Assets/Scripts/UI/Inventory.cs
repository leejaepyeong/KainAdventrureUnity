using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // �κ��丮 ���� �� �ٸ� ����(į�� ȸ�� ����) ����

    //�ʿ��� ������Ʈ
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting

    private Slot[] slots;   // ���� 20��

    public GameObject[] invenMenu;  // ��� / ������ / ����
    private int menuNum = 0;    // 0 : ��� 1 : ������ 2: ����

    private void Start()
    {
            slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }


    public void EquipOn()
    {
        menuNum = 0;
        ChangeMenu();
    }

    public void ItemOn()
    {
        menuNum = 1;
        ChangeMenu();
    }

    public void StructOn()
    {
        menuNum = 2;
        ChangeMenu();
    }

    void ChangeMenu()
    {
        foreach(GameObject menu in invenMenu)
        {
            menu.SetActive(false);
        }

        invenMenu[menuNum].SetActive(true);
    }

    //ȹ�� ������ �־��ֱ�
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
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
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

}
