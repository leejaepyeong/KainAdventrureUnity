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
                    break;

                case 1:
                    playerCastle[otherPlayer.myNum].Hp -= 20;
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
                    break;

                case 1:
                    playerCastle[otherPlayer.myNum].Hp += 10;
                    if (playerCastle[otherPlayer.myNum].Hp >= 100) playerCastle[otherPlayer.myNum].Hp = 100;
                    break;
            }
        }

    }

    void GroundOwner()
    {
        int myNum = curPlayer.myNum;

        if(owner == -1)
        {
            NetworkManager.NM.LogTxt.text = NetworkManager.NM.NicknameTxts[myNum].text + "가 땅을 구매했습니다";

            curPlayer.money -= 10;

            owner = myNum;

            PV.RPC("BuyRPC", RpcTarget.AllViaServer, myNum);

            curPlayer.playerHouse++;
            Debug.Log(curPlayer.playerHouse);
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

}
