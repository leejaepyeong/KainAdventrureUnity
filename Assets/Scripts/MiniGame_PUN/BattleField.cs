using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour
{
    public Spawn[] playerSpawn;


    public IEnumerator BattleStart()
    {
        playerSpawn[0].StartCoroutine("UnitSpawn");
        playerSpawn[1].StartCoroutine("UnitSpawn");

        yield return null;
    }


}
