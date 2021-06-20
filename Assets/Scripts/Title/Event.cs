using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public Action mainClear;

    public IsGameStart isGameStart;

    public int currentQuestIndex = 1;

    public GameObject cynemCam;
    public GameObject mainCam;

    int[] wallState = { 0 };
  
    public GameObject[] Walls;
    public QuestData[] questData;
    public GameObject[] cynematic;

    private void Start()
    {
        if (!isGameStart.isGameStart)
            StartCynematic();


        mainClear += WallDestroy;
        mainClear += SynematicOn;

        for (int i = 0; i < questData.Length; i++)
        {
            if(questData[i].isClear)
            {
                Destroy(Walls[i]);
            }
        }
    }

    private void Update()
    {

        if (questData[currentQuestIndex].isClear && !questData[currentQuestIndex].cynematic)
            mainClear();
    }

    void WallDestroy()
    {
        Destroy(Walls[currentQuestIndex],5f);
    }

    void SynematicOn()
    {
        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[currentQuestIndex].SetActive(true);
        questData[currentQuestIndex].cynematic = true;

        Invoke("CamOff", 8f);
    }

    void StartCynematic()
    {
        

        isGameStart.isGameStart = true; // game is Start First Cynematic

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[0].SetActive(true);

        Invoke("CamOff", 15f);
    }


    void CamOff()
    {
        cynematic[currentQuestIndex].SetActive(false);
        mainCam.SetActive(true);
        cynemCam.SetActive(false);
    }
}
