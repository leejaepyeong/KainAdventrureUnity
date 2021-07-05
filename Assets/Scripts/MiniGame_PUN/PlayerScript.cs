using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int curPos, myNum, money, playerHouse;

    public PlayerScript otherPlayer;

    // 플레이어 이동
    public IEnumerator Move(int diceNum)
    {
        

        yield return null;

        int[] movePos = new int[diceNum];
        bool isZero = false;

        for (int i = 0; i < movePos.Length; i++)
        {
            int plusNum = curPos + i + 1;
            if(plusNum > 15)
            {
                isZero = true;
                plusNum -= 16;
            }

            movePos[i] = plusNum;
        }

        for (int i = 0; i < movePos.Length; i++)
        {
            Vector3 targetPos = NetworkManager.NM.Pos[movePos[i]].position;

            while(true)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 7);

                if (transform.position == targetPos) break;
            }
        }

        if(isZero)
        {
            money += 30;
            NetworkManager.NM.LogTxt.text = myNum + "이 30$ 얻었습니다.";
        }

        curPos = movePos[movePos.Length - 1];

        NetworkManager.NM.Pos[curPos].GetComponent<GroundSwitch>().TypeSwitch(this, otherPlayer);
    }
  
}
