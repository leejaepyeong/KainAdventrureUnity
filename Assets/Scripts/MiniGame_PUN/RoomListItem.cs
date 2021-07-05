using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{

    RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
    }

    public void OnClick()
    {
        NetworkManager.NM.JoinTheRoom(info);
    }
}
