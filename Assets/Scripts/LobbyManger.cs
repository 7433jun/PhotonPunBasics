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

            // 룸 패널 생성 및 초기화
            Instantiate(roomPanel, listContent).GetComponent<RoomPanel>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // 사용 종료된 룸도 상태가 갱신된 룸이기때문에 리스트에 담겨져서 온다
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        List<RoomInfo> availableRoomList = new List<RoomInfo>();

        // 사용가능한 룸만 추출
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
        Debug.Log("룸 입장 성공");
        PhotonNetwork.LoadLevel("Room 1");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("로비 퇴장 성공");

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
