using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public Equipment equipment;    // 장비 중인 아이템

    public Item item;   // 슬롯 아이템
    public Image itemImage; // 장비템 이미지
    public Text equipTxt;   // 장비템 이름
    public int slotNum; // 장비템 자리

    UpgradeEquip upgradeEquip;

    private void Start()
    {
        upgradeEquip = FindObjectOfType<UpgradeEquip>();
    }

    private void Update()
    {
        MyItem();
    }

    // 슬롯에 장착 중인 아이템
    void MyItem()
    {
        item = equipment.equipItem[slotNum];
        itemImage.sprite = item.itemImage;
        equipTxt.text = item.itemName;
        
    }

    // 강화할 아이템 고르기
    public void ChooseItem()
    {
        upgradeEquip.UpgradeItem(item);
    }


}
