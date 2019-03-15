using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{

    int[] pos = { 0, 10, 20 };
    int idx = 0;
    float isDir = 0;
    bool isRightMove = false;
    bool isLeftMove = false;
    bool isidx = false;




    public GameObject tarGetCam;
    public Transform tarGetCamTr;
    void Start()
    {
        tarGetCamTr = tarGetCam.gameObject.GetComponent<Transform>();
    }

    void Update()
    {


        if (isLeftMove)
        {
            Debug.Log(idx);
            tarGetCamTr.Translate(Vector3.left * isDir * 15.0f * Time.deltaTime);
            if (isidx)   //0보다 작아서 인덱스가 2로 바뀌는 상태
            {
                if (tarGetCamTr.position.x >= pos[idx])
                {
                    isLeftMove = false;
                }
            }
            if (!isidx)
            {
                if (tarGetCamTr.position.x <= pos[idx])
                {
                    isLeftMove = false;
                }
            }
        }


        if (isRightMove)
        {
            tarGetCamTr.Translate(Vector3.left * isDir * 15.0f * Time.deltaTime);
            if (isidx)   //pos.Length보다 커서 인덱스가 0로 바뀌는 상태
            {
                if (tarGetCamTr.position.x <= pos[idx])
                {
                    isRightMove = false;
                }
            }
            if (!isidx)
            {
                if (tarGetCamTr.position.x >= pos[idx])
                {
                    isRightMove = false;
                }
            }
        }


    }

    public void Left_Button()
    {
        isLeftMove = true;
        if (idx <= 0)
        {
            idx = pos.Length - 1;
            isDir = -1; 
            isidx = true;
        }
        else
        {
            idx--;
            isDir = 1;
            isidx = false;
        }

        if(transform.parent.name == "Panel - EnemChar1")
        {
            SingletonMgr.EnemyNumber1 = idx;
        }
        else if (transform.parent.name == "Panel - EnemChar2")
        {
            SingletonMgr.EnemyNumber2 = idx;
        }
    }

    public void Right_Button()
    {
        isRightMove = true;

        if (idx >= pos.Length - 1)
        {
            idx = 0;
            isDir = 1;
            isidx = true;
        }
        else
        {
            idx++;
            isDir = -1;
            isidx = false;
        }

        if (transform.parent.name == "Panel - EnemChar1")
        {
            SingletonMgr.EnemyNumber1 = idx;
        }
        else if (transform.parent.name == "Panel - EnemChar2")
        {
            SingletonMgr.EnemyNumber2 = idx;
        }
    }

}
