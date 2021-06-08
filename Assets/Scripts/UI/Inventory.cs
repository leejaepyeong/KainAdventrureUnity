using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // ???????? ???? ?? ???? ????(???? ???? ????) ????

    public InventoryData[] inventoryData;
    //?????? ????????
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting

    //private Slot[] slots;   // ???? 20??


    public GameObject[] invenMenu;  // ???? / ?????? / ????
    public SlotPresenter[] slotPresenters;

    private int menuNum = 0;    // 0 : ???? 1 : ?????? 2: ????


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

    //???? ?????? ????????
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < inventoryData[(int)_item.itemType].item.Length; i++)
            {
                if(inventoryData[(int)_item.itemType].item[i] != null)
                {
                    if (inventoryData[(int)_item.itemType].item[i].itemName == _item.itemName)
                    {
                        inventoryData[(int)_item.itemType].itemCount[i]++;
                        slotPresenters[(int)_item.itemType].slots[i].SetSlotCount(1);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < inventoryData[(int)_item.itemType].item.Length; i++)
        {
            if (inventoryData[(int)_item.itemType].item[i] == null)
            {
                inventoryData[(int)_item.itemType].item[i] = _item;
                inventoryData[(int)_item.itemType].itemCount[i]++;

                slotPresenters[(int)_item.itemType].slots[i].AddItem(_item, _count);
                return;
            }
        }

    }

}
