using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager _PHOTON_MANAGER;
    private void Awake()
    {
        if (_PHOTON_MANAGER != null && _PHOTON_MANAGER != this)
            Destroy(this.gameObject);
        else
        {
            _PHOTON_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);

            PhotonConnect();
        }
    }

    public void PhotonConnect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion realizada corectaemnte");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("He implosionado porque " + cause);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Entrando al Lobby");
    }

    public void CreateRoom (string nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions { MaxPlayers = 2});
    }

    public void JoinRoom (string nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la sala: " + PhotonNetwork.CurrentRoom.Name + " con " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores conectados en ella");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("No he podido conectar a la ssala dado el error: " + returnCode + " que significa: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Ingame");
        }
    }

}
