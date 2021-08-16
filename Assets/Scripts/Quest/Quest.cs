using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public EnemyDB enemyDB;   // ???? ??
    public QuestData[] questDatas;
    public PlayerStatusData playerData;
    public MoneyData moneyData;

    public InventoryData[] inventoryData;

    public GameObject[] questTab;
    

    private void Update()
    {
        isSuccess();
        ResourceState();

    }



    public void MonsterDead(EnemyData enemy)
    {
        for (int i = 0; i < questDatas.Length; i++)
        {
            if(questDatas[i].isStart && questDatas[i].type == QuestData.Type.Kill)
            {
                    if (enemyDB.GetIDFrom(enemy) == questDatas[i].monsterId)
                    {
                        questDatas[i].Value--;
                    }
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
                        if(inventoryData[1].itemIDs[j] == questDatas[i].ResourseId)
                        {
                            questDatas[i].Value -= inventoryData[1].itemCount[j];
                            if (questDatas[i].Value <= 0) questDatas[i].Value = 0;
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

    //??
    public void GetReward(QuestData _questData)
    {
        playerData.currentExp += _questData.expReward;
        moneyData.Coin += _questData.coinReward;
    }


    public void MainQuestBtn()
    {
        questTab[0].SetActive(true);
        questTab[1].SetActive(false);

    }

    public void SubQuestBtn()
    {
        questTab[0].SetActive(false);
        questTab[1].SetActive(true);
    }
}
