using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GroundSwitch : MonoBehaviourPun
{
    public enum GroundType {GROUND, BUFF, DEBUFF, START }
    public GroundType groundType;

    public int price, owner;

    PlayerScript curPlayer, otherPlayer;
    public PlayerCastle[] playerCastle;

    PhotonView PV;
    TextMesh PriceTxt;
    GameObject[] Houses;
    string logTxt;

    private void Start()
    {
        PV = photonView;

        if(groundType == GroundType.GROUND)
        {
            PriceTxt = GetComponentInChildren<TextMesh>();
            PriceTxt.text = price.ToString();
            Houses = new GameObject[2] { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject};
        }
    }


    public void TypeSwitch(PlayerScript CurPlayer, PlayerScript OtherPlayer)
    {
        curPlayer = CurPlayer;
        otherPlayer = OtherPlayer;

        if(groundType == GroundType.GROUND)
        {
            GroundOwner();
            PV.RPC("AddPriceRPC", RpcTarget.AllViaServer);
        }

        else if(groundType == GroundType.BUFF)
        {
            int random = Random.Range(0, 2);

            switch(random)
            {
                case 0:
                    playerCastle[curPlayer.myNum].Hp += 20;
                    if (playerCastle[curPlayer.myNum].Hp >= 100) playerCastle[curPlayer.myNum].Hp = 100;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님의 성이 체력을 회복했습니다.";
                    PV.RPC("LogRPC",RpcTarget.AllViaServer, logTxt);
                    break;

                case 1:
                    playerCastle[otherPlayer.myNum].Hp -= 20;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님이 상대방에게 대미지를 입혔습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;
            }

            
        }

        else if (groundType == GroundType.DEBUFF)
        {
            int random = Random.Range(0, 2);

            switch (random)
            {
                case 0:
                    playerCastle[curPlayer.myNum].Hp -= 10;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님의 성이 체력을 잃었습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;

                case 1:
                    playerCastle[otherPlayer.myNum].Hp += 10;
                    if (playerCastle[otherPlayer.myNum].Hp >= 100) playerCastle[otherPlayer.myNum].Hp = 100;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님이 상대방 성의 체력을 회복했습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;
            }
        }

    }

    void GroundOwner()
    {
        int myNum = curPlayer.myNum;

        if(owner == -1)
        {
            logTxt = NetworkManager.NM.NicknameTxts[myNum].text + "가 땅을 구매했습니다";
            PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);

            curPlayer.money -= 10;

            owner = myNum;

            PV.RPC("BuyRPC", RpcTarget.AllViaServer, myNum);

            curPlayer.playerHouse++;
        }

        else if(owner != myNum)
        {
            curPlayer.money -= price;
            otherPlayer.money += price;
            NetworkManager.NM.LogTxt.text = NetworkManager.NM.NicknameTxts[myNum].text + "가 " + price + "을 잃었습니다";
            NetworkManager.NM.LogTxt.text = NetworkManager.NM.NicknameTxts[otherPlayer.myNum].text + "가 " + price + "을 얻었습니다";

        }
    }

    

    [PunRPC]
    void BuyRPC(int myNum)
    {
        Houses[myNum].SetActive(true);
    }

    [PunRPC]
    void AddPriceRPC()
    {
        price += 10;
        PriceTxt.text = price.ToString();
    }

    [PunRPC]
    public void LogRPC(string logTxt)
    {
        NetworkManager.NM.LogTxt.text = logTxt;
    }
}
