using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName ="User/PlayerData")]
public class PlayerStatusData : ScriptableObject
{
    public float maxHp;
    public float currentHp;

    public float maxMp;
    public float currentMp;

    public float meleeDamage;
    public float rangeDamage;
    public float resourceDamage;  //???? ???? ??????

    public float defence;

    public float hpRegen;
    public float mpRegen;

    public float hpRegenTime;
    public float mpRegenTime;

    public float maxExp;
    public float currentExp;
    public int level = 1;

    public int skillLevel = 0;

    public int stateCount;  // ?? ?? ? ?? ???


}
