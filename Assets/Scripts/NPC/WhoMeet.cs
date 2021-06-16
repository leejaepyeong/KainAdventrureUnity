using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoMeet : MonoBehaviour
{
    public static WhoMeet instanse;

   

    public GameObject[] npcPannels;

    public int number;

    public NPC npc;

    PlayerControleer player;

    private void Start()
    {
        instanse = this;
        player = FindObjectOfType<PlayerControleer>();
    }

    public void ClickOpen()
    {
        player.SetText(npc);

        npcPannels[number].SetActive(true);
    }
}
