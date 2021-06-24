using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Upgrade", menuName ="User/Upgrade")]
public class ItemUpgradeData : ScriptableObject
{
    public string itemName;

    public Item[] MaterialItems;  //필요 재료 아이템

    public int[] MaterialItemCount; // 필요 재료 갯수


}
