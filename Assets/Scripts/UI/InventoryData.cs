using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="Inventory", menuName ="User/Inventory")]
public class InventoryData : ScriptableObject
{
    public string InvenMenu;

    public int[] itemCount; // ������ ����
    public int[] itemIDs;   // ������ ���̵�

}
