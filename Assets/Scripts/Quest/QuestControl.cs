using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestControl : MonoBehaviour
{


    public int QuestId; //퀘스트 아이디

    public Text QuestText;
    public Text QuestDesc;
    public Text QuestValue;

 

    public Quest quest; //  퀘스트 정보 다루는 곳
    public QuestData[] questData;   // 퀘스트 데이터값

    public GameObject[] questList;  // 퀘스트 리스트
    public GameObject successQuest;
    public GameObject startQuest;

    private void Start()
    {
        QuestText.text = questData[QuestId].QuestName;
        QuestDesc.text = questData[QuestId].QuestDesc;
        QuestValue.text = questData[QuestId].MaxValue - questData[QuestId].Value + " / " + questData[QuestId].MaxValue;
    }

    private void Update()
    {
        CurrentQuestData();
    }

    void CurrentQuestData()
    { 
        QuestValue.text = questData[QuestId].MaxValue - questData[QuestId].Value + " / " + questData[QuestId].MaxValue;

        if(questData[QuestId].isReward)
        {
            successQuest.SetActive(false);
        }
        else
        {
            successQuest.SetActive(true);
        }

        if (questData[QuestId].isStart)
        {
            startQuest.SetActive(false);
        }
        else
        {
            startQuest.SetActive(true);
        }
    }

    public void ClickBtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < questList.Length; i++)
        {
            if(clickObject.name == questList[i].name)
            {
                QuestId = i;
            }
        }

        QuestText.text = questData[QuestId].QuestName;
        QuestDesc.text = questData[QuestId].QuestDesc;
        QuestValue.text = questData[QuestId].MaxValue - questData[QuestId].Value + " / " + questData[QuestId].MaxValue;
        
    }

    public void MissionStartBtn()
    {
        questData[QuestId].isStart = true;
    }

    public void GetReward()
    {
        if(questData[QuestId].isClear)
        {
            if(!questData[QuestId].isReward)
                quest.GetReward();

            questData[QuestId].isReward = true;
            
        }
    }

  

}
