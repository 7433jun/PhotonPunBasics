using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IPunObservable
{
    public static GameManager instance;

    [SerializeField] TextMesh textMesh;

    private int redScore;
    private int blueScore;

    void Awake()
    {
        PhotonNetwork.Instantiate("unitychan", new Vector3(0, 0.1f, -4f), Quaternion.identity);
    }


    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        textMesh.text = $"{redScore} : {blueScore}";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(redScore);
            stream.SendNext(blueScore);
        }
        else
        {
            redScore = (int)stream.ReceiveNext();
            blueScore = (int)stream.ReceiveNext();
        }
    }

    public void AddScore(string team)
    {
        switch (team)
        {
            case "Red":
                redScore++;
                break;
            case "Blue":
                blueScore++;
                break;
            default:
                break;
        }
    }
}
