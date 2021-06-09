using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Inventory", menuName ="User/Inventory")]
public class InventoryData : ScriptableObject
{
    public string InvenMenu;

    public int[] itemCount; // 아이템 갯수
    public int[] itemIDs;   // 아이템 아이디

}
