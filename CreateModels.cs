using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class CreateModels : MonoBehaviourPunCallbacks {

    public Transform camTargettr;
    public GameObject[] viewBear;
    GameObject SG_Player;
    GameObject MY_Player;
    GameObject PM_Player;
    
    Vector3 Myname = new Vector3(0, 0, 0.42f);
    Vector3 Player1 =new Vector3(0.6399999f, 0, 0.42f);
    Vector3 Player2 = new Vector3(1.51f, 0, 0.42f);

    public Button start_Button;
    bool pepleMax = false;

    void Start () {
        camTargettr = GetComponent<Transform>();
        SG_Player = viewBear[0];
        MY_Player = viewBear[1];
        PM_Player = viewBear[2];

        //    MyNameView();   // 내가 고른 캐릭터 보이기 

        StartCoroutine(timetime());
    }

    //초 시간을 안주면 생성이 제멋대로임
    IEnumerator timetime()
    {
        yield return new WaitForSeconds(5);
        photonView.RPC("PlayerNum", RpcTarget.All, SingletonMgr.PlayerNumber);
        //-----------------------------물어보기
        // 방장은 다 표시가 되지만 두번째(하나)랑 세번째(둘)는 앞서 들어온 사람의 정보를 얻지 못함 
        //혹시 들어올때마다 정보를 받을수 있는게 있는지.
    }


    private void Update()
    {
        //-----------------------------물어보기
        if (pepleMax) {                               //********마스터 일경우에만 !!! 나오게 해야.
            start_Button.gameObject.SetActive(true);
            pepleMax = false;
        }
    }


    //num값이 상대방 캐릭터 값을 가지고 있음
    [PunRPC]
    void PlayerNum(int num)
    {
        if(PhotonNetwork.PlayerList.Length == 1)
        {

            switch (num) // num이 1번이면 석겸 2번이면 명진 3번이면 민희
            {

                case 1:
                    GameObject obj = Instantiate(SG_Player, Myname, MY_Player.transform.rotation);
                    obj.SetActive(true);
                    break;

                case 2:
                    GameObject obj2 = Instantiate(MY_Player, Myname, MY_Player.transform.rotation);
                    obj2.SetActive(true);
                    break;

                case 3:
                    GameObject obj3 = Instantiate(PM_Player, Myname, MY_Player.transform.rotation);
                    obj3.SetActive(true);
                    break;
            }
        }

        if(PhotonNetwork.PlayerList.Length == 2)
        {
            switch (num) // num이 1번이면 석겸 2번이면 명진 3번이면 민희
            {

                case 1:
                    GameObject obj = Instantiate(SG_Player, Player1, MY_Player.transform.rotation);
                    obj.SetActive(true);
                    break;

                case 2:
                    GameObject obj2 = Instantiate(MY_Player, Player1, MY_Player.transform.rotation);
                    obj2.SetActive(true);
                    break;

                case 3:
                    GameObject obj3 = Instantiate(PM_Player, Player1, MY_Player.transform.rotation);
                    obj3.SetActive(true);
                    break;
            }
        }
        if (PhotonNetwork.PlayerList.Length == 3)
        {
            switch (num) // num이 1번이면 석겸 2번이면 명진 3번이면 민희
            {

                case 1:
                    GameObject obj = Instantiate(SG_Player, Player2, MY_Player.transform.rotation);
                    obj.SetActive(true);
                    break;

                case 2:
                    GameObject obj2 = Instantiate(MY_Player, Player2, MY_Player.transform.rotation);
                    obj2.SetActive(true);
                    break;

                case 3:
                    GameObject obj3 = Instantiate(PM_Player, Player2, MY_Player.transform.rotation);
                    obj3.SetActive(true);
                    break;

            }
            pepleMax = true;
        }
    }
}
       
   










 


