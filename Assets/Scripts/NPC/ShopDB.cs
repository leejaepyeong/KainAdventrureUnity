using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new db", menuName = "DB/Shop")]
public class ShopDB : ScriptableObject
{
    public Item[] items;
    public int[] isSold;

    public Item GetItemOrNull(int id)
    {
        if (id < items.Length)
            return items[id];

        return null;
    }

    // ¾ÆÀÌµð »Ì±â
    public int GetIDFrom(Item item)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i] == item)
                return i;
        }

        // NOT FOUND
        return 0;
    }
}
