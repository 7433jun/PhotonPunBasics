using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class LobbyManger : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomName;
    [SerializeField] private InputField maxPlayerCount;
    [SerializeField] private Button roomCreate;
    [SerializeField] private Button lobbyExit;
    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject roomPanel;

    private bool isRefresh;

    public void CreateRoomButton()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = int.Parse(maxPlayerCount.text);

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName.text, roomOptions);
    }

    public void ExitLobbyButton()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void RefreshButton()
    {
        isRefresh = true;

        PhotonNetwork.LeaveLobby();
    }

    public void DeleteAllPanel()
    {
        foreach (Transform transform in listContent)
        {
            Destroy(transform.gameObject);
        }
    }

    public void CreatePanel(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            Debug.Log(info.ToString());

            // �� �г� ���� �� �ʱ�ȭ
            Instantiate(roomPanel, listContent).GetComponent<RoomPanel>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // ��� ����� �뵵 ���°� ���ŵ� ���̱⶧���� ����Ʈ�� ������� �´�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        List<RoomInfo> availableRoomList = new List<RoomInfo>();

        // ��밡���� �븸 ����
        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                availableRoomList.Add(info);
            }
        }

        DeleteAllPanel();
        CreatePanel(availableRoomList);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����");
        PhotonNetwork.LoadLevel("Room 1");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("�κ� ���� ����");

        if (isRefresh)
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.LoadLevel("Launcher");
        }
    }

    public override void OnJoinedLobby()
    {
        isRefresh = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(PhotonNetwork.NetworkClientState);
        }
    }
}
