using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMovement : MonoBehaviour
{
    public CharacterController me;
    public GameObject torso, fadeout;
    public float speed, sprintspeed, rotationSpeed;
    public Transform armature;
    public Animation anim;
    public bool animstart, inaction, started, moving;
    public AudioSource footstep;
    public List<AudioSource> hurt;
    public List<AudioClip> footsteps;
    public Vector3 direction, position;
    void Start(){
        if(rotationSpeed == 0){
            rotationSpeed = 2.8f;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        direction = new Vector3(0.1f, 0f, 0.1f).normalized;
        PlayerPrefs.SetString("SaveRoom", "MansionExterior");
    }
    void FixedUpdate()
    {
        if (!inaction)
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * 5.0f;
            float vertical = Input.GetAxisRaw("Vertical") * 5.0f;
            direction = new Vector3(horizontal, 0f, vertical).normalized;
            transform.Rotate((armature.up * horizontal)/rotationSpeed);
            if (direction.magnitude >= 0.1f)
            {
                if (!anim.IsPlaying("boat moving"))
                {
                    anim.CrossFade("boat moving", 0.1f);
                    anim["boat moving"].speed = 0.5f;
                    anim.CrossFade("boat moving", 0.1f);
                }
                moving = true;
                Vector3 forward = torso.transform.forward;
                Vector3 backward = -torso.transform.forward;
                Vector3 right = torso.transform.right;
                Vector3 left = -torso.transform.right;
                if(Input.GetKey(KeyCode.W)){
                    me.Move((forward+=Physics.gravity) * speed * Time.deltaTime);
                }
                if(Input.GetKey(KeyCode.S)){
                    me.Move((backward+=Physics.gravity) * speed * Time.deltaTime);
                }
            }
            else
            {
                moving = false;
                anim.CrossFade("boat idle", 2);
            }
        }
    }
}