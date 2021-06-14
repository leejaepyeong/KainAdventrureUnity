using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ShopDB shopDB;   // 상점 데이터 배이스
    public MoneyData MyCoin;    // 소지금 확인

    public GameObject[] itemCheck;  // 슬롯 전부 에서 자기거 찾기용
    public int myNum;   // 슬롯 번호

    public Item item;
    public Image itemImage;

    public Inventory myInven;   // 자신 인벤토리

    [SerializeField]
    private Text ItemName;  // 아이템 이름 및  판매 상태
    [SerializeField]
    private Text showTxt;   //  코인 유무
    [SerializeField]
    private Text coinTxt;   //플레이어 코인 양

    
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

    void showItemCount()
    {
        coinTxt.text = MyCoin.Coin.ToString();

        if (shopDB.isSold[myNum] == 0)
            ItemName.text = "SOLD";
        else
            ItemName.text = shopDB.items[myNum].itemName + "\n" + shopDB.items[myNum].coinValue.ToString() + " Coin\n" + shopDB.isSold[myNum].ToString();
    }


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
            showTxt.text = "- 골드가 부족합니다 -";
            Invoke("DisappearItem", 1f);
        }

    }

    void DisappearItem()
    {
        showTxt.gameObject.SetActive(false);
    }


}
