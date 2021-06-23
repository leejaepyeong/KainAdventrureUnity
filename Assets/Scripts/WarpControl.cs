using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarpControl : MonoBehaviour
{
    public WarpZone[] warps;
    public GameObject CastleEnterZone;

    public PlayerControleer player;
    public QuestData[] questData;
    public Text actionTxt;

    int warpNum = 0;

    public GameObject[] warpImg;


    public void MoveHome()
    {
        warpImg[warpNum].SetActive(false);
        warpNum = 0;
        warpImg[warpNum].SetActive(true);
    }

    public void MoveMario()
    {
        warpImg[warpNum].SetActive(false);
        warpNum = 1;
        warpImg[warpNum].SetActive(true);
    }

    public void MoveWood()
    {
        warpImg[warpNum].SetActive(false);
        warpNum = 2;
        warpImg[warpNum].SetActive(true);
    }

    public void MoveCastle()
    {
        warpImg[warpNum].SetActive(false);
        warpNum = 3;
        warpImg[warpNum].SetActive(true);
    }

    public void EnterToCastleScene()
    {
        if(questData[2].isStart)
            player.transform.position = CastleEnterZone.transform.position;
        else
        {
            actionTxt.gameObject.SetActive(true);
            actionTxt.text = "You need Quest Start";

            Invoke("Disappear", 1f);
        }
    }

    void Disappear()
    {
        actionTxt.gameObject.SetActive(false);
    }

    public void Move()
    {
        if (warpNum == 3 && !questData[1].isClear)
            return;

        player.transform.position = warps[warpNum].transform.position;
    }
}
