using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new db", menuName = "DB/Item")]
public class ItemDB : ScriptableObject
{
    public Item[] items;    // 아이템 목록

    //아이디 맞는 아이템 뽑아오기
    public Item GetItemOrNull(int id)
    {
        if(id < items.Length)
            return items[id];

        return null;
    }

    // 아이디 뽑기
    public int GetIDFrom(Item item)
    {
        for(int i = 0; i < items.Length; ++i)
        {
            if (items[i] == item)
                return i;
        }

        // NOT FOUND
        return 0;
    }
}
