using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public EnemyData enemyData;
    public DiceUnitData diceUnitData;

    public string enemyTag;

    private void Start()
    {
        if (tag == "Player1")
            enemyTag = "Player2";
        else
            enemyTag = "Player1";
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag)
        {
            Destroy(gameObject);

            if(other.GetComponent<DiceUnit>() != null)
            {
                DiceUnit otherUnit = other.GetComponent<DiceUnit>();
                otherUnit.hp -= otherUnit.deffence - diceUnitData.damage;
            }

            else
            {
                PlayerCastle otherCastle = other.GetComponent<PlayerCastle>();
                otherCastle.Hp -= diceUnitData.damage;
            }
        }
    }

}
