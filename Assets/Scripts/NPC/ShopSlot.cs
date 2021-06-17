using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ShopDB shopDB;   // Shop Data Base
    public MoneyData MyCoin;    // Player Money Data

    public GameObject[] itemCheck;  // Items
    public int myNum;   // item number

    public Item item;
    public Image itemImage;

    public Inventory myInven;   // equip inventory

    [SerializeField]
    private Text ItemName;  // item Name
    [SerializeField]
    private Text showTxt;   //  Text
    [SerializeField]
    private Text coinTxt;   // Your Coin Txt


    // Set item number;
    private void Start()
    {
        for (int i = 0; i < itemCheck.Length; i++)
        {
            if(itemCheck[i].name == gameObject.name)
            {
                myNum = i+1;


                itemImage.sprite = shopDB.items[myNum].itemImage;

                item = shopDB.items[myNum];

                showItemCount();
            }
        }
    }

    // show hot many items;
    void showItemCount()
    {
        coinTxt.text = MyCoin.Coin.ToString();

        if (shopDB.isSold[myNum] == 0)
            ItemName.text = "SOLD";
        else
            ItemName.text = shopDB.items[myNum].itemName + "\n" + shopDB.items[myNum].coinValue.ToString() + " Coin\n" + shopDB.isSold[myNum].ToString();
    }

    // item purchase
    public void Purchase()
    {
        if(MyCoin.Coin >= shopDB.items[myNum].coinValue)
        {
            MyCoin.Coin -= shopDB.items[myNum].coinValue;

            showItemCount();

            myInven.AcquireItem(shopDB.items[myNum]);

        }
        else
        {
            showTxt.gameObject.SetActive(true);
            showTxt.text = "- You need Money! -";
            Invoke("DisappearItem", 1f);
        }

    }

    // item txt  disappear
    void DisappearItem()
    {
        showTxt.gameObject.SetActive(false);
    }


}
