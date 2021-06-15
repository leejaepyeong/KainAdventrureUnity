using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class ItemEffectDataBase : MonoBehaviour
{
    public Equipment equipment;

    public PlayerStatusData playerStatusData;

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
        if (_item.itemType == Item.ItemType.Equipment)
        {
            switch(_item.equipType)
            {
                case Item.EquipType.Sword:
                    equipment.equipItem[(int)Item.EquipType.Sword] = _item;
                    break;
                case Item.EquipType.Arrow:

                    equipment.equipItem[(int)Item.EquipType.Arrow] = _item;
                    break;
                case Item.EquipType.Armor:
                    equipment.equipItem[(int)Item.EquipType.Armor] = _item;
                    break;
            }
        }

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

