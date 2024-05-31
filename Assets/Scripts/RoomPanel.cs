using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviourPun
{
    [SerializeField] private Text panelRoomName;
    [SerializeField] private Text panelPlayerCount;
    public string roomName;

    public void JoinRoomButton()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetInfo(string name, int current, int max)
    {
        roomName = name;
        panelRoomName.text = $"Name : {name}";
        panelPlayerCount.text = $"{current} / {max}";
    }
}
