using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="EquipState",menuName ="User/Equip")]
public class Equipment : ScriptableObject
{
    public Item[] equipItem;    //  Equip Item
}
