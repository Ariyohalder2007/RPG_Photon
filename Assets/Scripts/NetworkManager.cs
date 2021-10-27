using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
  public int maxPlayers;

  public static NetworkManager Instance { get; private set; }

  private void Awake()
  {

    if (Instance != null && Instance!=this) 
    {
      gameObject.SetActive(false);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    
  }


  private void Start()
  {
    PhotonNetwork.ConnectUsingSettings();
  }

  public override void  OnConnectedToMaster()
  {
    PhotonNetwork.JoinLobby();
    Debug.Log("Joined Master");
  }

  public void JoinRoom(string roomName)
  {
    PhotonNetwork.JoinRoom(roomName);
  }

  public void CreateRoom(string roomName)
  {
    RoomOptions options = new RoomOptions
    {
      MaxPlayers = (byte)maxPlayers
    };
    PhotonNetwork.CreateRoom(roomName, options);
  }

  public void ChangeScene(string sceneName)
  {
   PhotonNetwork.LoadLevel(sceneName); 
  }

  

  public override void OnJoinedRoom()
  {
    Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
  }
}
