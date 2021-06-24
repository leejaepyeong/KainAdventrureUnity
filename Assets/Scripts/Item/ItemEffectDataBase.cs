using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class ItemEffectDataBase : MonoBehaviour
{
    public Equipment equipment; // 장비 목록
    public Inventory inventroy; // 인벤토리


    public PlayerStatusData playerStatusData;   // 플레이어 상태 (회복 장비장착 등 상호작용)

    // 아이템 이름 설명 이미지 보여주기
    public GameObject dataPannel;
    public Text dataTitleTxt;
    public Text dataDescTxt;
    public Image dataImg;



    public void ShowToolTip(Item _item)
    {
        dataPannel.SetActive(true);
        dataTitleTxt.text = _item.itemName;
        dataDescTxt.text = _item.itemDesc;
        dataImg.sprite = _item.itemImage;
        
    }

    public void HideToolTip()
    {
        dataPannel.SetActive(false);
    }

    public void UseItem(Item _item, int _id)
    {
        // 장비 장착
        if (_item.itemType == Item.ItemType.Equipment)
        {
            switch(_item.equipType)
            {
                case Item.EquipType.Sword:
                    playerStatusData.meleeDamage -= equipment.equipItem[1].value * _item.Upgrade;
                    equipment.equipItem[(int)Item.EquipType.Sword] = _item;
                    playerStatusData.meleeDamage += _item.value * _item.Upgrade;
                    break;
                case Item.EquipType.Arrow:
                    playerStatusData.rangeDamage -= equipment.equipItem[2].value * _item.Upgrade;
                    equipment.equipItem[(int)Item.EquipType.Arrow] = _item;
                    playerStatusData.rangeDamage += _item.value * _item.Upgrade;
                    break;
                case Item.EquipType.Armor:
                    playerStatusData.defence -= equipment.equipItem[3].value * _item.Upgrade;
                    equipment.equipItem[(int)Item.EquipType.Armor] = _item;
                    playerStatusData.defence += _item.value * _item.Upgrade;
                    break;
            }

            inventroy.EquipInven();
        }

        // 소비템 사용
       if (_item.itemType == Item.ItemType.Ingredient && _item.isUsed)
        {
            switch (_item.useType)
            {
                case Item.UseType.Hp:
                    playerStatusData.currentHp += playerStatusData.maxHp / _item.value;
                    break;
                case Item.UseType.Mp:
                    playerStatusData.currentMp += playerStatusData.maxMp / _item.value;
                    break;
            }
        }

    }



}

