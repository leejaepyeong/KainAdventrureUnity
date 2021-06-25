using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 업그레이드 필요한 데이터 관리
[CreateAssetMenu(fileName ="Upgrade", menuName ="User/Upgrade")]
public class ItemUpgradeData : ScriptableObject
{
    public string itemName; // 아이템 이름

    public Item[] MaterialItems;  //필요 재료 아이템

    public int[] MaterialItemCount; // 필요 재료 갯수

    public InventoryData inventoryData; //  아이템 인벤토리
    public ItemDB itemDB;   // 아이템 데이터베이스 (아이디값 얻기용)



    // 재료 충분한지
    public bool isEnoughItem()
    {
        for (int i = 0; i < MaterialItems.Length; i++)
        {
            int itemID = itemDB.GetIDFrom(MaterialItems[i]);

            for (int j = 0; j < inventoryData.itemIDs.Length; j++)
            {
                if(itemID == inventoryData.itemIDs[j])
                {
                    if (MaterialItemCount[i] <= inventoryData.itemCount[j])
                        return true;
                }
            }
        }

        return false;
    }

    // 재료 소비
    public void UseMaterial()
    {
        for (int i = 0; i < MaterialItems.Length; i++)
        {
            int itemID = itemDB.GetIDFrom(MaterialItems[i]);

            for (int j = 0; j < inventoryData.itemIDs.Length; j++)
            {
                if (itemID == inventoryData.itemIDs[j])
                {
                    inventoryData.itemCount[j] -= MaterialItemCount[i];
                }
            }
        }
    }

}
