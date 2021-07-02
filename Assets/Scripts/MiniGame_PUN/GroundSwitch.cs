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
    public GameObject buyPannel;

    public int price, owner;
    private int purchseHouse = 0;

    PlayerScript curPlayer, otherPlayer;

    PhotonView PV;
    TextMesh PriceTxt;
    GameObject[] Houses;

    private void Start()
    {
        PV = photonView;

        if(groundType == GroundType.GROUND)
        {
            PriceTxt = GetComponentInChildren<TextMesh>();
            Houses = new GameObject[4] { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject,
                transform.GetChild(2).gameObject, transform.GetChild(3).gameObject };
        }
    }


    public void TypeSwitch(PlayerScript CurPlayer, PlayerScript OtherPlayer)
    {
        curPlayer = CurPlayer;
        otherPlayer = OtherPlayer;

        if(groundType == GroundType.GROUND)
        {
            GroundOwner();
        }

        else if(groundType == GroundType.BUFF)
        {
            int random = Random.Range(0, 2);

            switch(random)
            {
                case 0:

                    break;

                case 1:
                    break;
            }
        }
    }

    void GroundOwner()
    {
        int myNum = curPlayer.myNum;

        if((owner == -1 || owner == myNum) && purchseHouse <= 1)
        {
            PV.RPC("HouseRPC", RpcTarget.MasterClient);
        }

        else if(owner != myNum)
        {
            curPlayer.money -= price;
            NetworkManager.NM.LogTxt.text = NetworkManager.NM.NicknameTxts[myNum].text + "가 " + price + "을 잃었습니다";
            NetworkManager.NM.LogTxt.text = NetworkManager.NM.NicknameTxts[otherPlayer.myNum].text + "가 " + price + "을 얻었습니다";

        }
    }

    // 집 사기
    void AddGround()
    {
        int myNum = curPlayer.myNum;

        PV.RPC("AddPriceRPC",RpcTarget.AllViaServer);
        PV.RPC("BuyRPC", RpcTarget.AllViaServer, myNum);

        curPlayer.playerHouse++;

        purchseHouse++;
    }

    void CancleBuy()
    {
        buyPannel.SetActive(false);
    }

   

    [PunRPC]
    void HouseRPC(int myNum)
    {
        buyPannel.SetActive(true);
    }

    [PunRPC]
    void BuyRPC(int myNum)
    {
        curPlayer.money -= price;

        owner = myNum;
        Houses[myNum + purchseHouse].SetActive(true);
    }

    [PunRPC]
    void AddPriceRPC()
    {
        price += 10;
        PriceTxt.text = price.ToString();
    }

}
