using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    //private void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        Debug.Log(PhotonNetwork.NetworkClientState);
    //        Debug.Log(PhotonNetwork.MasterClient.ToString());
    //    }
    //}

    public void ExitRoomButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���漭�� ���� ����");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
