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

    public int currentQuestIndex = 1;

    public GameObject cynemCam;
    public GameObject mainCam;

    int[] wallState = { 0 };
  
    public GameObject[] Walls;
    public QuestData[] questData;
    public GameObject[] cynematic;
    public GameObject lastCynematic;
    public GameObject vitcoryPannel;

    public GameObject endingZone;

    public AudioSource audio;

    private void Start()
    {
        if (instanse == null)
            instanse = this;

        audio = GetComponent<AudioSource>();

        if (!isGameStart.isGameStart)
            StartCynematic();
        else
            GameManager.instance.isGameOn = true;

        mainClear += WallDestroy;
        mainClear += SynematicOn;

        for (int i = 1; i < questData.Length - 1; i++)
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

        for (int i = currentQuestIndex; i < questData.Length - 1; i++)
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
        GameManager.instance.isGameOn = false;

        currentQuestIndex--;

        isGameStart.isGameStart = true; // game is Start First Cynematic

        mainCam.SetActive(false);
        cynemCam.SetActive(true);
        cynematic[0].SetActive(true);

        Invoke("CamOff", 32f);

        

    }


    // cam Change
    void CamOff()
    {
        cynematic[currentQuestIndex].SetActive(false);
        mainCam.SetActive(true);
        cynemCam.SetActive(false);
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
