using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new db", menuName = "DB/Enemy")]
public class EnemyDB : ScriptableObject
{
    public EnemyData[] enemys;    // 적 목록

    //아이디 맞는 적 뽑아오기
    public EnemyData GetItemOrNull(int id)
    {
        if (id < enemys.Length)
            return enemys[id];

        return null;
    }

    // 아이디 뽑기
    public int GetIDFrom(EnemyData enemy)
    {
        for (int i = 0; i < enemys.Length; ++i)
        {
            if (enemys[i] == enemy)
                return i;
        }

        // NOT FOUND
        return 0;
    }
}
