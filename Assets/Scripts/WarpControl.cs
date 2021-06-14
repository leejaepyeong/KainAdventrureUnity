using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpControl : MonoBehaviour
{
    public WarpZone[] warps;

    public PlayerControleer player;

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

    public void Move()
    {
        player.transform.position = warps[warpNum].transform.position;
    }
}
