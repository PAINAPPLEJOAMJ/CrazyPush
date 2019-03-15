using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MJ_PlayerCtrl : MonoBehaviour {


    private float moveSpeed = 2.0f;                         // 플레이어의 이동속도
    private float jumpSpeed = 5.0f;                         // 플레이어의 점프속도
    private Animator anim;
    private Transform tr;

    public JoystickCtrl joystick;      // 조이스틱 스크립트

    private UICtrl uiCtrl;                                  // UICtrl 스크립트
    private Rigidbody rb;                                   // 플레이어의 리지드바디
    public GameObject guard;                                // 플레이어의 가드 효과


    void Start()
    {
        
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        // Joystick 오브젝트에서 JostickCtrl 스크립트 정보를 가지고 옴
        joystick = GameObject.Find("Joystick").GetComponent<JoystickCtrl>();

        // UIMgr 오브젝트에서 UICtrl 스크립트 정보를 가지고 옴
        uiCtrl = GameObject.Find("UIMgr").GetComponent<UICtrl>();
    }

    void Update()
    {
        // 입력 값에 따른 플레이어의 이동과 공격
        PlayerInput();
    }

    // 플레이어의 입력값을 받을 함수
    public void PlayerInput()
    {
        // 조이스틱 + 키보드 이동 /////////////////////////////////////////////////

        float h = joystick.GetHorizontalValue() + Input.GetAxisRaw("Horizontal");
        float v = joystick.GetVerticalValue() + Input.GetAxisRaw("Vertical");

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        anim.SetBool("WALK", false);

        // x축값과 y축값이 0이 아닐 때
        if (h != 0 || v != 0)
        {
            anim.SetBool("WALK", true);

            // 캐릭터 회전
            tr.rotation = Quaternion.LookRotation(moveDir);

            // 앞으로 이동
            tr.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }

        //////////////////////////////////////////////////////////////////////////

        // 키보드용 버튼 입력(공격1, 공격2, 방어, 점프) //////////////////////////
        if (CrossPlatformInputManager.GetButtonDown("ATTACK1"))
        {
            Attack1();
        }

        if (CrossPlatformInputManager.GetButtonDown("ATTACK2"))
        {
            Attack2();
        }

        if (CrossPlatformInputManager.GetButton("DEFEND"))
        {
            Defend();
        }
        else
        {
            CancelDefend();
        }

        if (CrossPlatformInputManager.GetButtonDown("JUMP"))
        {
            Jump();
        }
        //////////////////////////////////////////////////////////////////////////
    }


    public void Attack1()
    {
        // 스킬 포인트가 30 이상이면 스킬사용 가능
        if (uiCtrl.GetSp >= 30)
        {
            uiCtrl.SkillDecrease(30);  //스킬 포인트 감소
            anim.SetTrigger("ATTACK1");
        }

    }

    public void Attack2()
    {
        // 스킬 포인트가 30 이상이면 스킬사용 가능
        if (uiCtrl.GetSp >= 60)
        {
            uiCtrl.SkillDecrease(60);  //스킬 포인트 감소
            anim.SetTrigger("ATTACK2");
        }
    }

    public void Defend()
    {
        // 스킬 포인트가 0보다 크면
        if (uiCtrl.GetSp > 0)
        {
            Debug.Log(uiCtrl.GetSp);
            uiCtrl.IsDefend = true;         // 방어중
            anim.SetBool("DEFEND", true);
            guard.SetActive(true);          // 방어 효과 시작
            rb.isKinematic = true;
        }
        else
        {
            CancelDefend();
        }
    }

    private void CancelDefend()
    {
        uiCtrl.IsDefend = false;            // 방어x
        anim.SetBool("DEFEND", false);
        guard.SetActive(false);              // 방어 효과 끝
        rb.isKinematic = false;
    }

    public void Jump()
    {
        RaycastHit hit;

        // 플레이어가 바닥 위에 있는가?
        if (Physics.Raycast(tr.position, Vector3.down, out hit, 0.5f))
        {
            if (hit.transform.tag == "FLOOR")
            {
                anim.SetTrigger("JUMP");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }
}
