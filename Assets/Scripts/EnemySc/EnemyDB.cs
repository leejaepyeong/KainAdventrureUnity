using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new db", menuName = "DB/Enemy")]
public class EnemyDB : ScriptableObject
{
    public EnemyData[] enemys;    // �� ���

    //���̵� �´� �� �̾ƿ���
    public EnemyData GetItemOrNull(int id)
    {
        if (id < enemys.Length)
            return enemys[id];

        return null;
    }

    // ���̵� �̱�
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
