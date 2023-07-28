using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fire : MonoBehaviour
{
    public Animation anim;
    public ParticleSystem part;
    public float bulletspeed;
    public Transform gun;
    public GameObject shot, mc;
    public AudioSource dryshot;
    public Animation thisanim;
    public Animation mcanim;
    public LayerMask enemy;
    public void animreset()
    {
        //TODO make gun return to proper rotation after animation
        thisanim["deaglelid"].speed = -1.0f;
        thisanim["deaglelid"].time = gameObject.GetComponent<Animation>()["deaglelid"].length;
        thisanim.Play("deaglelid");
    }
    public void shoot()
    {
        mc = GameObject.Find("Character");
        if (PlayerPrefs.GetInt("AmmoLoaded", 0) != 0)
        {
            if(mc.GetComponent<ThirdPersonController>() == null){
                mc.GetComponent<ThirdPersonControllerHarbour>().idleblock = true;
            }else{
                mc.GetComponent<ThirdPersonController>().idleblock = true;
            }
            mcanim.Stop("shoot");
            thisanim.Stop();
            thisanim.Play("fire");
            mcanim["shoot"].layer = 1;
            mcanim["runtorso"].layer = 0;
            mcanim.Play("shoot");
            GameObject audio = Instantiate(shot, gun);
            audio.GetComponent<AudioSource>().Play();
            Destroy(audio, 4f);
            part.Play();
            PlayerPrefs.SetInt("AmmoLoaded", PlayerPrefs.GetInt("AmmoLoaded", 0) - 1);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, enemy))
            {
                if(hit.transform.gameObject.tag == "Monster"){
                    Debug.Log("hit enemy");
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
                    hit.transform.gameObject.GetComponent<BaseEnemyAI>().hit();
                }
            }else{
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green);
            }
        }
        else
        {
            dryshot.Play();
        }
    }
}