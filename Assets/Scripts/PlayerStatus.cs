using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    public int maxHp;
    public int currentHp;

    public int maxMp;
    public int currentMp;

    public int meleeDamage;
    public int rangeDamage;
    public int resourceDamage;

    public int defence;

    public int hpRegen;
    public int mpRegen;

    public float hpRegenTime;
    public float mpRegenTime;

    public int maxExp;
    public int currentExp;
    public int level = 1;


    //필요 컴포넌트
    public PlayerControleer player;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        LevelUp();
    }


    void LevelUp()
    {
        if(currentExp >= maxExp)
        {
            level++;
            GameManager.instance.stat += 3;

            currentExp =currentExp - maxExp;
            maxExp *= 2;
        }    
    }

    public void StateDamageUp()
    {
        if(GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;

            meleeDamage += 2;
            rangeDamage += 1;
            resourceDamage += 1;

        }
    }

    public void StateDefenceUp()
    {
        if (GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;
            defence += 1;
        }
    }

    public void StateHpMpUp()
    {
        if (GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;

            maxHp += 10;
            currentHp += 10;

            maxMp += 5;
            currentMp += 5;
        }
    }



}
