using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField nickName;

    private bool isConnecting;

    void Start()
    {
        nickName.text = PlayerPrefs.GetString("Name");
    }

    public void Connect()
    {
        isConnecting = true;

        if (CheckNickName())
        {
            PlayerPrefs.SetString("Name", nickName.text);
        }
        else
        {
            Debug.Log("�г����� �Է��ϼ���.");
            return;
        }

        // ���� Ŭ���̾�Ʈ�� ���� ������ Ŭ���̾�Ʈ�� ����ȭ ����
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.NickName = nickName.text;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public bool CheckNickName()
    {
        if (nickName.text == string.Empty)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.Log("OnConnectedToMaster() : ���漭�� ���� ����");
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby() : �κ� ���� ����");
        PhotonNetwork.LoadLevel("Lobby");
    }
}
