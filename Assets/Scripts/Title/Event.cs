using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public Action mainClear;
    public Action Ending;

    public IsGameStart isGameStart;

    public int currentQuestIndex = 1;

    public GameObject cynemCam;
    public GameObject mainCam;

    int[] wallState = { 0 };
  
    public GameObject[] Walls;
    public QuestData[] questData;
    public GameObject[] cynematic;
    public GameObject lastCynematic;

    private void Start()
    {
        if (!isGameStart.isGameStart)
            StartCynematic();

        mainClear += WallDestroy;
        mainClear += SynematicOn;

        for (int i = 1; i < questData.Length; i++)
        {
            if(!questData[i].isClear)
            {
                currentQuestIndex = i;
                break;
            }

            Destroy(Walls[i]);
        }
    }

    private void Update()
    {

        for (int i = currentQuestIndex; i < questData.Length; i++)
        {
            if (questData[i].isClear && !questData[i].cynematic)
            {
                mainClear();
            }
                
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
        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[currentQuestIndex].SetActive(true);
        questData[currentQuestIndex].cynematic = true;

        Invoke("CamOff", 8f);
        
    }

    // First Game Start Cynematic
    void StartCynematic()
    {
        currentQuestIndex--;

        isGameStart.isGameStart = true; // game is Start First Cynematic

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[0].SetActive(true);

        Invoke("CamOff", 15f);

        
    }


    // cam Change
    void CamOff()
    {
        cynematic[currentQuestIndex].SetActive(false);
        mainCam.SetActive(true);
        cynemCam.SetActive(false);
        currentQuestIndex++;
    }


    public void EndingCynematic()
    {
        Ending();

        lastCynematic.SetActive(true);
    }
}
