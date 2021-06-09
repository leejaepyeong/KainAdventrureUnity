using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public EnemyDB enemyDB;
    public QuestData[] questDatas;
    public PlayerStatusData playerData;
    public MoneyData moneyData;

    public InventoryData[] inventoryData;

    private void Update()
    {
        isSuccess();
    }

    void Alram(int _num)
    {
        questDatas[_num].isStart = true;
    }

    public void MonsterDead(EnemyData enemy)
    {
        for (int i = 0; i < questDatas.Length; i++)
        {
            if (questDatas[i].isStart && questDatas[i].type == QuestData.Type.Kill && enemyDB.GetIDFrom(enemy) == questDatas[i].monsterId)
            {
                questDatas[i].Value--;
            }
        }
        
    }

    public void ResourceState()
    {
        for (int i = 0; i < questDatas.Length; i++)
        {
            if (questDatas[i].isStart && questDatas[i].type == QuestData.Type.Resource)
            {
                if(questDatas[i].itemType == QuestData.ItemType.Resource)
                {
                    for (int j = 0; j < inventoryData[1].itemIDs.Length; j++)
                    {
                        if(inventoryData[1].itemIDs[i] == questDatas[i].ResourseId)
                        {

                        }
                    }
                }
                else if(questDatas[i].itemType == QuestData.ItemType.Struct)
                {
                    for (int j = 0; j < inventoryData[2].itemIDs.Length; j++)
                    {
                        if (inventoryData[2].itemIDs[i] == questDatas[i].ResourseId)
                        {

                        }
                    }
                }
            }
        }

            
    }

    void isSuccess()
    {
        for (int i = 0; i < questDatas.Length; i++)
        {
            if(questDatas[i].isStart)
            {
                if(questDatas[i].Value == 0)
                    Success(i);
            }
        }
    }

    void Success(int _num)
    {
        questDatas[_num].isClear = true;
    }

    //보상받기
    public void GetReward()
    {
        playerData.currentExp += questDatas[0].expReward;
        moneyData.Coin += questDatas[0].coinReward;
    }

}
