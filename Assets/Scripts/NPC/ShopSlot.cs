using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public ShopDB shopDB;   // ���� ������ ���̽�
    public MoneyData MyCoin;    // ������ Ȯ��

    public GameObject[] itemCheck;  // ���� ���� ���� �ڱ�� ã���
    public int myNum;   // ���� ��ȣ

    public Item item;
    public Image itemImage;

    public Inventory myInven;   // �ڽ� �κ��丮

    [SerializeField]
    private Text ItemName;  // ������ �̸� ��  �Ǹ� ����
    [SerializeField]
    private Text showTxt;   //  ���� ����
    [SerializeField]
    private Text coinTxt;   //�÷��̾� ���� ��

    
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
            showTxt.text = "- ��尡 �����մϴ� -";
            Invoke("DisappearItem", 1f);
        }

    }

    void DisappearItem()
    {
        showTxt.gameObject.SetActive(false);
    }


}
