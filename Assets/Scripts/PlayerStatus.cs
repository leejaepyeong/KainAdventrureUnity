using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public PlayerStatusData playerData;

    

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
        playerData.currentHp = playerData.maxHp;
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
        levelTxt.text = playerData.level.ToString();
        hPTxt.text = playerData.currentHp.ToString() + " / " + playerData.maxHp.ToString();
        mPTxt.text = playerData.currentMp.ToString() + " / " + playerData.maxMp.ToString();
        mAttTxt.text = playerData.meleeDamage.ToString();
        rAttTxt.text = playerData.rangeDamage.ToString();
        reAttTxt.text = playerData.resourceDamage.ToString();
        defTxt.text = playerData.defence.ToString();
    }
    void LevelUp()
    {
        if(playerData.currentExp >= playerData.maxExp)
        {
            playerData.level++;
            GameManager.instance.stat += 3;


            playerData.currentExp = playerData.currentExp - playerData.maxExp;
            playerData.maxExp *= 2;
        }    
    }

    public void StateDamageUp()
    {
        if(GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;

            playerData.meleeDamage += 2;
            playerData.rangeDamage += 1;
            playerData.resourceDamage += 1;

        }
    }

    public void StateDefenceUp()
    {
        if (GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;
            playerData.defence += 1;
        }
    }

    public void StateHpMpUp()
    {
        if (GameManager.instance.stat > 0)
        {
            GameManager.instance.stat--;

            playerData.maxHp += 10;
            playerData.currentHp += 10;

            playerData.maxMp += 5;
            playerData.currentMp += 5;
        }
    }


    public void IncreaseHp(int _data)
    {
        playerData.currentHp += _data;
    }
    public void IncreaseMp(int _data)
    {
        playerData.currentMp += _data;
    }


    public void HpBarControl()
    {
        hpBar.fillAmount = playerData.currentHp / playerData.maxHp;
    }

    public void MpBarControl()
    {
        mpBar.fillAmount = playerData.currentMp / playerData.maxMp;
    }

    public void ExpBarControl()
    {
        expBar.fillAmount = playerData.currentExp / playerData.maxExp;
    }


}
