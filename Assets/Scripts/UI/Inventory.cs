using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // 장비창 활성화 유무


    public ItemDB[] SomeDB;
    public InventoryData[] inventoryData;
    public MoneyData moneyData;
   
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting


    public Text coin;
    public GameObject[] invenMenu;  // 0 : 장비 1 : 아이템 2: 구조
    public SlotPresenter[] slotPresenters;

    private int menuNum = 0;    // 0 : 장비 1 : 아이템 2: 구조


    private void Start()
    {
        CheckInvenList();
    }

    public void CheckInvenList()
    {
        coin.text = moneyData.Coin.ToString() + " Gold";

        for (int i = 0; i < slotPresenters.Length; i++)
        {
            for (int j = 0; j < inventoryData[i].itemIDs.Length; j++)
            {

                if(inventoryData[i].itemIDs[j] != 0)
                    slotPresenters[i].slots[j].AddItem(SomeDB[i].items[inventoryData[i].itemIDs[j]], inventoryData[i].itemCount[j]);

                /*
                if(inventoryData[i].item[j] != null)
                    slotPresenters[i].slots[j].AddItem(inventoryData[i].item[j], inventoryData[i].itemCount[j]);
                */
            }
        }
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

    // 아이템 목록 바꾸기 장비 아이템 가구
    void ChangeMenu()
    {
        foreach(GameObject menu in invenMenu)
        {
            menu.SetActive(false);
        }

        invenMenu[menuNum].SetActive(true);
    }

    //아이템 수집
    public void AcquireItem(Item _item, int _count = 1)
    {
        int num = (int)_item.itemType;


        if (Item.ItemType.Coin == _item.itemType)
        {
            moneyData.Coin += _item.coinValue;
            coin.text = moneyData.Coin.ToString() + " Gold";
            return;
        }

        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < inventoryData[num].itemIDs.Length; i++)
            {
                if(inventoryData[num].itemIDs[i] != 0)
                {
                    if (SomeDB[num].items[inventoryData[num].itemIDs[i]].itemName == _item.itemName)
                    {
                        inventoryData[num].itemCount[i]++;
                        slotPresenters[num].slots[i].SetSlotCount(1);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < inventoryData[num].itemIDs.Length; i++)
        {
            var itemID = SomeDB[num].GetIDFrom(_item);

            if (inventoryData[num].itemIDs[i] == 0 && itemID != 0)
            {
                inventoryData[num].itemIDs[i] = itemID;
                inventoryData[num].itemCount[i]++;
                slotPresenters[num].slots[i].AddItem(_item, 1);

                return;
            }
        }

    }

}
