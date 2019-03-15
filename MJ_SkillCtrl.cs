using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_SkillCtrl : MonoBehaviour
{
    public enum SkillType
    {
        Push, PowerPush
    };

    private Rigidbody rb;
    public SkillType skilltype;
    public GameObject hartBroken1;
    public GameObject hartBroken2;


    private AudioSource musicPlayer;
    public AudioClip SkillSound;

    private void Start()
    {
        musicPlayer = Camera.main.GetComponent<AudioSource>();


        rb = GetComponent<Rigidbody>();

        //1번 스킬타입이면 
        if (skilltype == SkillType.Push)
        {
            StartCoroutine(moveHart1());   

        }
        //2번 스킬 타입이면
        else if (skilltype == SkillType.PowerPush)
        {
            StartCoroutine(moveHart2());
        }
        Destroy(this.gameObject, 4.0f);
    }
    IEnumerator moveHart1()             //하트에 닿으면 밀리는 스킬
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 300f);
    }
     IEnumerator moveHart2()           // 하트에 닿으면 하트가 쪼개지면서 좌,우로 밀리는 스킬
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 600f);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Player" || coll.collider.tag == "Enemy")
        {
            if (skilltype == SkillType.Push)
            {
                Destroy(this.gameObject);
                Vector3 yyyy = new Vector3(0, 0.5f, 0);
                Instantiate(hartBroken1, coll.transform.position + yyyy, coll.transform.rotation);

                SoundMgr.playSound(SkillSound, musicPlayer);

                coll.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 400f);
            }
            if (skilltype == SkillType.PowerPush)
            {
                Destroy(this.gameObject);
                Vector3 yyyy = new Vector3(0, 0.5f, 0);
                Instantiate(hartBroken2, coll.transform.position + yyyy, coll.transform.rotation);

                SoundMgr.playSound(SkillSound, musicPlayer);

                coll.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * 500f);

            }
        }

    }


}
