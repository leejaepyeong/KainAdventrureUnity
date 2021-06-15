using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// ?????? ?????? ????
//create?? ???? ?? ????????????

// scrip?? ???????? ????????
public class Item : ScriptableObject
{
    //??? ??
    public string itemName;
    [TextArea]  // ??? ??
    public string itemDesc; //??? ??
    public ItemType itemType;
    public Sprite itemImage;

    public GameObject itemPrefab; // ??? ???

    public string weaponType;   // ?? ??

    public int coinValue;
    public int value;

    public enum ItemType
    {
        Equipment,
        Ingredient,
        Struct,
        Coin
    }

    public enum EquipType
    {
        None,
        Sword,
        Arrow,
        Armor
    }

    public EquipType equipType;

    public enum UseType
    {
        None,
        Hp,
        Mp
    }

    public UseType useType;

    public bool isUsed = false;
    public bool isEquip = false;
}
