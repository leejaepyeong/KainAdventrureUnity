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

    public int price, owner, rank = 0;

    PlayerScript curPlayer, otherPlayer;
    public PlayerCastle[] playerCastle;
    public GameObject[] buffEffect;

    PhotonView PV;
    TextMesh PriceTxt;
    GameObject[] Houses;
    GameObject[] RankTile;
    string logTxt;

    private void Start()
    {
        PV = photonView;

        if(groundType == GroundType.GROUND)
        {
            PriceTxt = GetComponentInChildren<TextMesh>();
            PriceTxt.text = price.ToString();
            Houses = new GameObject[2] { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject};
            RankTile = new GameObject[2] { transform.GetChild(2).gameObject, transform.GetChild(3).gameObject };
        }


        NetworkManager.NM.GameReset = startReset;
    }

    void startReset()
    {
        foreach (GameObject house in Houses)
            house.SetActive(false);

        foreach (GameObject rankTile in RankTile)
            rankTile.SetActive(false);

        price = 10;
        PriceTxt.text = price.ToString();
        rank = 0;
        owner = -1;
    }

    public void TypeSwitch(PlayerScript CurPlayer, PlayerScript OtherPlayer)
    {
        curPlayer = CurPlayer;
        otherPlayer = OtherPlayer;

        if (groundType == GroundType.GROUND)
        {
            GroundOwner();

            if(owner != otherPlayer.myNum)
            PV.RPC("AddPriceRPC", RpcTarget.AllViaServer);
        }

        else if(groundType == GroundType.BUFF)
        {
            int random = Random.Range(1, 3);

            switch(random)
            {
                case 1:
                    StartCoroutine(HealBuff(curPlayer));
                    playerCastle[curPlayer.myNum].Hp += 20;
                    if (playerCastle[curPlayer.myNum].Hp >= 100) playerCastle[curPlayer.myNum].Hp = 100;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님의 성이 체력을 회복했습니다.";
                    PV.RPC("LogRPC",RpcTarget.AllViaServer, logTxt);
                    break;

                case 2:
                    StartCoroutine(AttackBuff());
                    playerCastle[otherPlayer.myNum].Hp -= 20;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님이 상대방에게 대미지를 입혔습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;
            }



        }

        else if (groundType == GroundType.DEBUFF)
        {
            int random = Random.Range(1, 3);

            switch (random)
            {
                case 1:
                    StartCoroutine(BoomBuff());
                    playerCastle[curPlayer.myNum].Hp -= 10;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님의 성이 체력을 잃었습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;

                case 2:
                    StartCoroutine(HealBuff(otherPlayer));
                    playerCastle[otherPlayer.myNum].Hp += 10;
                    if (playerCastle[otherPlayer.myNum].Hp >= 100) playerCastle[otherPlayer.myNum].Hp = 100;
                    logTxt = NetworkManager.NM.NicknameTxts[curPlayer.myNum].text + "님이 상대방 성의 체력을 회복했습니다.";
                    PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
                    break;
            }

        }

    }


    IEnumerator AttackBuff()
    {
        GameObject attackBuff = PhotonNetwork.Instantiate(buffEffect[0].name, playerCastle[curPlayer.myNum].transform.position, playerCastle[curPlayer.myNum].transform.rotation);
        Rigidbody attRB = attackBuff.GetComponent<Rigidbody>();
        attRB.velocity = playerCastle[curPlayer.myNum].transform.forward * 2.5f;

        yield return new WaitForSeconds(1.5f);
        PhotonNetwork.Destroy(attackBuff);

        GameObject hitBuff = PhotonNetwork.Instantiate(buffEffect[1].name, playerCastle[otherPlayer.myNum].transform.position, playerCastle[curPlayer.myNum].transform.rotation);

        yield return new WaitForSeconds(0.8f);
        PhotonNetwork.Destroy(buffEffect[1]);

    }

    IEnumerator HealBuff(PlayerScript targetPlayer)
    {
        GameObject healBuff = PhotonNetwork.Instantiate(buffEffect[2].name, playerCastle[targetPlayer.myNum].transform.position, playerCastle[targetPlayer.myNum].transform.rotation);

        yield return new WaitForSeconds(1f);

        PhotonNetwork.Destroy(healBuff);

    }

    IEnumerator BoomBuff()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject boomBuff = PhotonNetwork.Instantiate(buffEffect[0].name, playerCastle[curPlayer.myNum].transform.position, playerCastle[curPlayer.myNum].transform.rotation);

        yield return new WaitForSeconds(0.5f);
        GameObject boomBuff2 = PhotonNetwork.Instantiate(buffEffect[1].name, playerCastle[curPlayer.myNum].transform.position, playerCastle[curPlayer.myNum].transform.rotation);


        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(boomBuff);
        PhotonNetwork.Destroy(boomBuff2);
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
        else if(owner == myNum)
        {
            if (rank == 0)
            {
                rank++;
                curPlayer.playerUpgrde++;
            }
            else
                return;

            logTxt = NetworkManager.NM.NicknameTxts[myNum].text + "가 땅을 업그레이드 했습니다";
            PV.RPC("LogRPC", RpcTarget.AllViaServer, logTxt);
            curPlayer.money -= 10;
            PV.RPC("BuyRPC", RpcTarget.AllViaServer, myNum);
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
        if(rank == 0)
        Houses[myNum].SetActive(true);

        if (rank == 1)
            RankTile[myNum].SetActive(true);
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
