using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    public int maxHp;
    public int currentHp;

    public int maxMp;
    public int currentMp;

    public int meleeDamage;
    public int rangeDamage;
    public int resourceDamage;  //자원 채취 대미지

    public int defence;

    public int hpRegen;
    public int mpRegen;

    public float hpRegenTime;
    public float mpRegenTime;

    public int maxExp;
    public int currentExp;
    public int level = 1;

    public Image hpBar;
    public Image mpBar;
    public Image expBar;

    public Text levelTxt;
    public Text hPTxt;
    public Text mPTxt;
    public Text mAttTxt;
    public Text rAttTxt;
    public Text reAttTxt;
    public Text defTxt;

    //필요 컴포넌트
    public PlayerControleer player;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        LevelUp();
        HpBarControl();
        MpBarControl();
        ExpBarControl();
        StatusInfo();
    }

    void StatusInfo()
    {
        levelTxt.text = level.ToString();
        hPTxt.text = currentHp.ToString() + " / " + maxHp.ToString();
        mPTxt.text = currentMp.ToString() + " / " + maxMp.ToString();
        mAttTxt.text = meleeDamage.ToString();
        rAttTxt.text = rangeDamage.ToString();
        reAttTxt.text = resourceDamage.ToString();
        defTxt.text = defence.ToString();
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


    public void IncreaseHp(int _data)
    {
        currentHp += _data;
    }
    public void IncreaseMp(int _data)
    {
        currentMp += _data;
    }


    public void HpBarControl()
    {
        hpBar.fillAmount = (float)currentHp / maxHp;
    }

    public void MpBarControl()
    {
        mpBar.fillAmount = (float)currentMp / maxMp;
    }

    public void ExpBarControl()
    {
        expBar.fillAmount = (float)currentExp / maxExp;
    }


}
