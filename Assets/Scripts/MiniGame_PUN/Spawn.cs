using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawn : MonoBehaviourPun
{
    
    public DiceUnit[] diceUnits;
    public PlayerScript player;



    public IEnumerator UnitSpawn()
    {
        yield return null;


        for (int i = 0; i < player.playerHouse; i++)
        {
            int num = i % 3;

            GameObject spwanUnit = PhotonNetwork.Instantiate(diceUnits[i].name, transform.position, Quaternion.identity);
            spwanUnit.tag = player.tag;
        }
    }
}
