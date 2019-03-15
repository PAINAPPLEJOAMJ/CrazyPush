using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_FireCtrl : MonoBehaviour
{
    public GameObject Skill1;
    public GameObject Skill1_Aura;
    public GameObject Skill2;
    public GameObject Skill2_Aura;

    private Transform firePos;


    private AudioSource musicPlayer;        // 음악플레이어
    public AudioClip SkillSound;            // 스킬 사운드



    void Start()
    {
        firePos = transform.Find("MJ_Fire_pos").GetComponent<Transform>();

        musicPlayer = Camera.main.GetComponent<AudioSource>();
    }

    void Fire1()  
    {
        Instantiate(Skill1, firePos.position, firePos.rotation);
        SoundMgr.playSound(SkillSound, musicPlayer); // 스킬 사운드 실행
    }
    void Fire_Aura1()
    {
        Instantiate(Skill1_Aura, firePos.position, firePos.rotation);
        
    }

    void Fire2()
    {
        Instantiate(Skill2, firePos.position, firePos.rotation);
        SoundMgr.playSound(SkillSound, musicPlayer); // 스킬 사운드 실행
    }

    void Fire_Aura2()
    {
        Instantiate(Skill2_Aura, firePos.position, firePos.rotation);
    }
}
