using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    public DiceUnit[] diceUnits;
    PlayerScript player;

    public IEnumerator UnitSpawn()
    {
        yield return null;

        for (int i = 0; i < player.playerHouse; i++)
        {
            int num = i % 3;

            DiceUnit spwanUnit = Instantiate<DiceUnit>(diceUnits[0], transform.position, Quaternion.identity);
            spwanUnit.gameObject.tag = gameObject.tag;
        }
    }
}
