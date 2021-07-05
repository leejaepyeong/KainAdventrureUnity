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

    public Text StatusTxt;

    [Header("DisconnectPanel")]
    public GameObject DisconnectPanel;
    public InputField NicknameInput;

    [Header("LobyPanel")]
    public GameObject LobyPannel;
    public InputField RoomInput;
    public Text WelcomeTxt;
    public Text LobbyInfoTxt;
    public Button[] CellBtn;
    public Button NextBtn;
    public Button PreviousBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public GameObject InitGameBtn, RollBtn;
    public Text[] NicknameTxts;
    public GameObject[] ArrowImages;

    /*
     public Text[] ChatTxt;
    public InputField ChatInput;
     */

    [Header("Boarder")]
    public Dice diceScript;
    public BattleField battleField;
    public Transform[] Pos;
    public PlayerScript[] Players;
    public Text[] MoneyTexts;
    public Text LogTxt;
    public int myNum, turn;

    [Header("ETC")]
    PhotonView PV;
    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;
    
    public PlayerScript MyPlayer;

    public bool isGameStart;

    private void Start()
    {
#if(!UNITY_ANDROID)
    Screen.SetResolution(960,540,false);
#endif

        PV = photonView;

    }

    private void Update()
    {
        // 상태
       // StatusTxt.text = PhotonNetwork.NetworkClientState.ToString();
       if(PhotonNetwork.InLobby)
        LobbyInfoTxt.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "로비 / " + PhotonNetwork.CountOfPlayers + "접속";
    }

    // 접속하기
    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = NicknameInput.text;
        PhotonNetwork.ConnectUsingSettings();       
    }

    // 접속 및 로비입장
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // 접속 끊기 버튼
    public void DisConnectBtn()
    {
        PhotonNetwork.Disconnect();
    }

    // 접속 끊기
    public override void OnDisconnected(DisconnectCause cause)
    {
        LobyPannel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    // 로비 입장
    public override void OnJoinedLobby()
    {
        SetPanel(LobyPannel);
        WelcomeTxt.text = PhotonNetwork.LocalPlayer.NickName + "님 환영합니다.";
        myList.Clear();
    }

    // 방 만들기
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomInput.text == "" ? "Room" + Random.Range(0,100) : RoomInput.text, new RoomOptions {MaxPlayers = 2 });
    }

    // 랜덤 방 입장
    public void JoinRandomRoomBtn()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // 방 생성 실패 > 랜덤 방 생성
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

    // 방참가 실패 > 랜덤방 생성
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

    bool master()
    {
        return PhotonNetwork.LocalPlayer.IsMasterClient;
    }

    public void JoinTheRoom(RoomInfo _info)
    {
        PhotonNetwork.JoinRoom(_info.Name);
    }

    // 방참가
    public override void OnJoinedRoom()
    {
        SetPanel(RoomPanel);
        if (master())
        {
            InitGameBtn.SetActive(true);
        }
    }


    public void LeaveRoomBtn()
    {
        PhotonNetwork.LeaveRoom();
        InitGameBtn.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        SetPanel(LobyPannel);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string message = otherPlayer.NickName + "이 퇴장했습니다";

        PV.RPC("LogRPC", RpcTarget.AllViaServer, message);

        RoomRenewal();
    }

    void RoomRenewal()
    {
        //SetPanel(RoomPanel);

        if (master())
        {
            InitGameBtn.SetActive(true);
        }
    }

    public void InitGame()
    {
        // 현재 방의 플레이어 수 Check
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;

        RollBtn.SetActive(true);
        InitGameBtn.SetActive(false);
        PV.RPC("InitGameRPC", RpcTarget.AllViaServer);
    }

    //방리스트 갱신
    // < -2 왼쪽 버튼 , > -1 오른쪽 버튼 , 셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }
    
    // 로비에서 방 상태
    void MyListRenewal()
    {
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : (myList.Count / CellBtn.Length) + 1;

        // 이전 , 다음 버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + " / " + myList[multiple + i].MaxPlayers : "";
            CellBtn[i].GetComponent<RoomListItem>().SetUp(myList[multiple + i]);

        }
    }

    // 방리스트 업데이트
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;

        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }


    // 패널 열기
    void SetPanel(GameObject curPanel)
    {
        DisconnectPanel.SetActive(false);
        LobyPannel.SetActive(false);
        RoomPanel.SetActive(false);

        curPanel.SetActive(true);
    }
    

    [PunRPC]
    void InitGameRPC()
    {
        for (int i = 0; i < 2; i++)
        {
            NicknameTxts[i].text = PhotonNetwork.PlayerList[i].NickName;

            // 각 네트워크의 매니저 구분 위해  로컬 플레이어한테 myNum 입력
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                myNum = i;
        }
    }

    public void Roll()
    {
        PV.RPC("RollRPC",RpcTarget.MasterClient);
    }

    [PunRPC]
    void LogRPC(string logTxt)
    {
        LogTxt.text = logTxt;
    }

    [PunRPC]
    void RollRPC()
    {
        StartCoroutine(RollCo());
    }

    [PunRPC]
    void EndRollRPC(int money0, int money1)
    {
        turn = turn == 0 ? 1 : 0;

        for (int i = 0; i < 2; i++)
        {
            ArrowImages[i].SetActive(i == turn);
        }

        RollBtn.SetActive(myNum == turn);

        MoneyTexts[0].text = money0.ToString();
        MoneyTexts[1].text = money1.ToString();



       // if (money0 <= 0 || money1 >= 300) LogTxt.text = NicknameTxts[1].text + "이 승리하셨습니다";
       // else if (money1 <= 0 || money0 >= 300) LogTxt.text = NicknameTxts[0].text + "이 승리하셨습니다";
    }

    IEnumerator RollCo()
    {

        //방장만 함수 호출
        // 해당 함수를 다 돌아야 이후로 넘어감
        yield return StartCoroutine(diceScript.Roll());
        yield return StartCoroutine(Players[turn].Move(diceScript.num));
        yield return new WaitForSeconds(0.2f);

        if(turn == 1)
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(battleField.BattleStart());
        }
        


        PV.RPC("EndRollRPC", RpcTarget.AllViaServer, Players[0].money, Players[1].money);

    }

}
