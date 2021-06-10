using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "New Enemy/enemy")]
public class EnemyData : ScriptableObject
{
    // ?? ????
    public enum EnemyType
    {
        normal,
        Undead,
        Boss,
    }

    public string enemyName;

    public EnemyType enemyType;

    //????
    public int hp;
    public int mp;
    public int damage;
    public int defense;
    public float delay;
    public float moveSpeed;
    public int skillDamage;


    public int hpRegen;
    public int mpRegen;
    public float regenTime;

    public int Exp;

    public float targetRange;
    // ????
    [TextArea] public string enemyData;

   
}
