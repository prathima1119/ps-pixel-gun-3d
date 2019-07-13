﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;
    // Start is called before the first frame update
    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConnectToPhotonServer()
    {
        if(!PhotonNetwork.IsConnected)
        {

            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }
        
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName+"connected to photon server.");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet.");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name + "" + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0,10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }


}
