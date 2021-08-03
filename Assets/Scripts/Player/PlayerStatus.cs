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
    public Text skillTxt;

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
        skillTxt.text = playerData.skillLevel.ToString();

    }
    void LevelUp()
    {
        if(playerData.currentExp >= playerData.maxExp)
        {
            playerData.level++;
            playerData.stateCount += 4;

            playerData.currentExp = playerData.currentExp - playerData.maxExp;
            playerData.maxExp *= 2;
        }    
    }

    public void StateDamageUp()
    {
        if(playerData.stateCount > 0)
        {
            playerData.stateCount--;

            playerData.meleeDamage += 4;
            playerData.rangeDamage += 2;
            playerData.resourceDamage += 1;

        }
    }

    public void StateDefenceUp()
    {
        if (playerData.stateCount > 0)
        {
            playerData.stateCount--;
            playerData.defence += 2;

            playerData.skillLevel++;
            
        }
    }

    public void StateHpMpUp()
    {
        if (playerData.stateCount > 0)
        {
            playerData.stateCount--;

            playerData.maxHp += 20;
            playerData.currentHp += 20;

            playerData.maxMp += 10;
            playerData.currentMp += 10;
        }
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
