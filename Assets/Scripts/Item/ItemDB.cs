using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new db", menuName = "DB/Item")]
public class ItemDB : ScriptableObject
{
    public Item[] items;    // ������ ���

    //���̵� �´� ������ �̾ƿ���
    public Item GetItemOrNull(int id)
    {
        if(id < items.Length)
            return items[id];

        return null;
    }

    // ���̵� �̱�
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
