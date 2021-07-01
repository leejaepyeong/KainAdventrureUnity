using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public static Event instanse;

    public Action mainClear;

    public IsGameStart isGameStart;

    public int currentQuestIndex;   // 시네머신 카운트

    public GameObject cynemCam; // 시네머신용 카메라
    public GameObject mainCam;  // 메인 카메라

    int[] wallState = { 0 };
  
    public GameObject[] Walls;  // 벽 제거
    public QuestData[] questData;   // 각 시네머신 관련 퀘스트
    public GameObject[] cynematic;  // 시네머진 모음
    public GameObject lastCynematic;    // 엔딩 시네머신
    public GameObject vitcoryPannel;    // 승리 패널

    public GameObject endingZone;

    public AudioSource audio;

    public PlayerControleer player;
    public GameObject boss;
    public Transform[] position;

    private void Start()
    {
        if (instanse == null)
            instanse = this;


        mainClear += WallDestroy;
        mainClear += SynematicOn;

        if (!isGameStart.isGameStart)
            StartCynematic();
        else
        {
            GameManager.instance.isGameOn = true;

            for (int i = 1; i < questData.Length - 1; i++)
            {
                if (!questData[i].isClear)
                {
                    currentQuestIndex = i;
                    break;
                }

                Destroy(Walls[i]);
            }
        }
            

        

        
    }

    private void Update()
    {

        for (int i = 1; i < questData.Length - 1; i++)
        {
            if (questData[i].isClear && !questData[i].cynematic)
            {
                mainClear();
            }    
        }

        if(questData[3].isClear)
        {
            endingZone.SetActive(true);
        }
        
    }

    // Wall Destroy
    void WallDestroy()
    {
         Destroy(Walls[currentQuestIndex],6f);
    }

    // cynematic On
    void SynematicOn()
    {
        GameManager.instance.isGameOn = false;

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[currentQuestIndex].SetActive(true);
        questData[currentQuestIndex].cynematic = true;

        Invoke("CamOff", 8f);
        
    }

    // First Game Start Cynematic
    void StartCynematic()
    {
        player.transform.position = position[0].position;

        GameManager.instance.isGameOn = false;

        currentQuestIndex = 0;

        isGameStart.isGameStart = true; // game is Start First Cynematic

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[0].SetActive(true);

        Invoke("CamOff", 32f);

        
    }

    public void LastBossCinematic()
    {
        player.transform.position = position[0].position;
        boss.SetActive(false);

        GameManager.instance.isGameOn = false;

        currentQuestIndex = 3;

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[3].SetActive(true);

        Invoke("CamOff", 29f);
    }


    // cam Change
    void CamOff()
    {
        cynematic[currentQuestIndex].SetActive(false);
        mainCam.SetActive(true);
        cynemCam.SetActive(false);

        if (currentQuestIndex == 3)
        {
            player.transform.position = position[1].position;
            boss.SetActive(true);
        }
        else if(currentQuestIndex == 0)
        {
            player.transform.position = position[2].position;
        }
            

        currentQuestIndex++;

        GameManager.instance.isGameOn = true;

        
    }


    public void EndingCynematic()
    {
        vitcoryPannel.SetActive(false);
        questData[3].cynematic = true;

        mainCam.SetActive(false);
        cynemCam.SetActive(true);

        lastCynematic.SetActive(true);

        Invoke("MovetoTitle", 21f);
    }


    void MovetoTitle()
    {
        lastCynematic.SetActive(false);
        mainCam.SetActive(true);
        cynemCam.SetActive(false);

        //Allreset();

        SceneManager.LoadScene("Title");
    }

}
