using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager NM;
    private void Awake()
    {
        NM = this;
    }

    [Header("DisconnectPanel")]
    public GameObject DisconnectPanel;
    public InputField NicknameInput;

    [Header("LobyPanel")]

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public GameObject InitGameBtn, RollBtn;
    public Text[] NicknameTxts;
    public GameObject[] ArrowImages;

    [Header("Boarder")]
    public Dice diceScript;
    public Transform[] Pos;
    public PlayerScript[] Players;
    public Text[] MoneyTexts;
    public Text LogTxt;

    public int myNum, turn;
    PhotonView PV;
    
    public PlayerScript MyPlayer;

    public bool isGameStart;

    private void Start()
    {
#if(!UNITY_ANDROID)
    Screen.SetResolution(960,540,false);
#endif

        PV = photonView;

    }

    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        PhotonNetwork.ConnectUsingSettings();       
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {

    }

    [PunRPC]
    void InitGame()
    {
        for (int i = 0; i < 2; i++)
        {
            NicknameTxts[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void Roll()
    {
        PV.RPC("RollRPC",RpcTarget.AllViaServer);
    }

    [PunRPC]
    void RollRPC()
    {
        StartCoroutine(RollCo());

    }

    IEnumerator RollCo()
    {
        yield return null;
    }

}
