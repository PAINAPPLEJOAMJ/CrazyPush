using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
public class PhotonInit : MonoBehaviourPunCallbacks {

   


    private string gameVersion = "0.0.1";
    public string userId = "nadayo";
    public byte maxPlayer = 3;

     Text txtUserId;


  //  public SceneMgr scenemgr;  sceneload해보고 안되면 그때 직접 넣어서 해보자

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // 같은방 동기화

        if (!PhotonNetwork.IsConnected)
        {
            OnLogin();

        }
    }

    void Start () {
        //유저 아이디
      //  txtUserId.text = PlayerPrefs.GetString("USER_ID", "Gom" + Random.Range(1, 20));

	}



    #region SELF_CALLBACK_FUNCTIONS
    public void OnLogin()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = PlayerPrefs.GetString("USER_ID", "Gom" + Random.Range(1, 20));

        PhotonNetwork.ConnectUsingSettings();

        PlayerPrefs.SetString("USER_ID", PhotonNetwork.NickName);

    }
    // 방생성
    public void OnCreateRoomClick()
    {
        
        PhotonNetwork.CreateRoom("playground" + Random.Range(1, 999).ToString()
                                 , new RoomOptions { MaxPlayers = this.maxPlayer });
        Debug.Log("Create");

    }
    //방 들어가기
    public void OnJoinRandomRoomClick()
    {

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("room Join");

    }
    #endregion

    #region PHOTON_CALLBACK_FUNCTIONS
    //포톤 접속
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Master");
        PhotonNetwork.JoinLobby();

    }

    //로비 입장
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    //랜덤 룸 입장 실패시
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed Join room !!!");
    }

    //룸입장 
    public override void OnJoinedRoom()
    {


        Debug.Log("Joined Room !!!");
        SceneManager.LoadScene("2-2-2.Room");
        
    }

/*
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        

        Debug.Log(PhotonNetwork.PlayerList.Length );

        if (PhotonNetwork.PlayerList.Length == 2)
        {

            PhotonNetwork.Instantiate("player1", Vector3.zero, Quaternion.identity, 0);
        }
    }
*/
}

        #endregion




/*    
        ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
        ht.Add("Model", SingletonMgr.PlayerNumber);
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht, null, null);
*/