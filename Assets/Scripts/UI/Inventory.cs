using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // ???????? ???? ?? ???? ????(???? ???? ????) ????
    
    //?????? ????????
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting

    //private Slot[] slots;   // ???? 20??


    public GameObject[] invenMenu;  // ???? / ?????? / ????
    public SlotPresenter[] slotPresenters;

    private int menuNum = 0;    // 0 : ???? 1 : ?????? 2: ????

    private void Start()
    {
           // slots[] = go_SlotsParent[].GetComponentsInChildren<Slot>();
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

    //???? ?????? ????????
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            /*
            foreach (Slot slot in slotPresenters[(int)_item.itemType].slots.)
                {

                
                if (slot.item != null)
                    {
                        if (slot.item.itemName == _item.itemName)
                        {
                            slot.SetSlotCount(_count);
                            return;
                        }
                    }
                }
            */
                for (int j = 0; j < slotPresenters[(int)_item.itemType].slots.Count; j++)
                {
                    if (slotPresenters[(int)_item.itemType].slots[j].item != null)
                    {
                        if (slotPresenters[(int)_item.itemType].slots[j].item.itemName == _item.itemName)
                        {
                        slotPresenters[(int)_item.itemType].slots[j].SetSlotCount(_count);
                            return;
                        }
                    }
                }
        }
/*
        foreach (Slot slot in slotPresenters[0].slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(_item, _count);
                return;
            }
        }
*/

               
                for (int i = 0; i < slotPresenters[(int)_item.itemType].slots.Count; i++)
                {
                    if (slotPresenters[(int)_item.itemType].slots[i].item == null)
                    {
                slotPresenters[(int)_item.itemType].slots[i].AddItem(_item, _count);
                        return;
                    }
                }
                
            }

}
