using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    public MoneyData moneyData;
    public PlayerStatusData playerStatus;

    private int charge = 500;   // 힐 비용

    public Text coinTxt;
    public Text txt;

    private void Update()
    {
        coinTxt.text = moneyData.Coin.ToString();
    }
    public void HealPlayer()
    {
        StopCoroutine(startHeal());
        StartCoroutine(startHeal());
        
    }

    IEnumerator startHeal()
    {
        if (moneyData.Coin >= charge)
        {
            moneyData.Coin -= charge;
            playerStatus.currentHp = playerStatus.maxHp;
            playerStatus.currentMp = playerStatus.maxMp;
        }
        else
        {
            txt.gameObject.SetActive(true);
            txt.text = "이게 돈도 없으면서 왔어??";
            yield return new WaitForSeconds(1.5f);
            txt.gameObject.SetActive(false);

        }
        yield return null;
    }


}
