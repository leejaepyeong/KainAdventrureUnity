using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // Equip State

    public PlayerStatusData playerData; //  player Data
    public ItemDB[] SomeDB; // Item  DataBase
    public InventoryData[] inventoryData;   //  inventroy Data
    public MoneyData moneyData; //  My Coin Data
    public Equipment equip; //  Equip Data

    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;  // grid Setting


    public Text coin;
    public GameObject[] invenMenu;  // 0 : equip 1 : item 2: struct
    public SlotPresenter[] slotPresenters;
    public Text[] tabTxt;   // ?? ??????

    public Image[] equipItems;  //  ?? ?? ??? ??? 

    private int menuNum = 0;    // 0 : equip 1 : item 2: struct


    private void Start()
    {
        tabTxt[0].color = Color.red;
        CheckInvenList();
        EquipInven();
    }



    // invenList
    public void CheckInvenList()
    {
        coin.text = moneyData.Coin.ToString() + " Gold";

        for (int i = 0; i < slotPresenters.Length; i++)
        {
            for (int j = 0; j < inventoryData[i].itemIDs.Length; j++)
            {

                if(inventoryData[i].itemIDs[j] != 0)
                    slotPresenters[i].slots[j].AddItem(SomeDB[i].items[inventoryData[i].itemIDs[j]], inventoryData[i].itemCount[j]);

            }
        }
    }

    public void EquipInven()
    {
        for (int i = 1; i < equip.equipItem.Length; i++)
        {
            equipItems[i-1].sprite = equip.equipItem[i].itemImage;
        }
        
    }

    public void EquipOn()
    {
        tabTxt[menuNum].color = Color.white;
        menuNum = 0;
        ChangeMenu();
        tabTxt[menuNum].color = Color.red;
    }

    public void ItemOn()
    {
        tabTxt[menuNum].color = Color.white;
        menuNum = 1;
        ChangeMenu();
        tabTxt[menuNum].color = Color.green;
    }

    public void StructOn()
    {
        tabTxt[menuNum].color = Color.white;
        menuNum = 2;
        ChangeMenu();
        tabTxt[menuNum].color = Color.blue;
    }

    // Menu Change
    void ChangeMenu()
    {
        foreach(GameObject menu in invenMenu)
        {
            menu.SetActive(false);
        }

        invenMenu[menuNum].SetActive(true);
    }

    // Get Item > Add or Create
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
