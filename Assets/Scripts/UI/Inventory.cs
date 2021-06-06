using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // 인벤토리 설정 시 다른 동작(캄라 회전 공격) 정지

    //필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting

    private Slot[] slots;   // 슬롯 20개

    public GameObject[] invenMenu;  // 장비 / 아이템 / 가구
    private int menuNum = 0;    // 0 : 장비 1 : 아이템 2: 가구

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

    //획득 아이템 넣어주기
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
