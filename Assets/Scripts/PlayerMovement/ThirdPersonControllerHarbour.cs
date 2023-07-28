using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdPersonControllerHarbour : MonoBehaviour
{
    public CharacterController me;
    public GameObject gun, torso, fadeout, cam1, cam2, freelook;
    public float speed, sprintspeed, rotationSpeed;
    public GameObject light, oceanAmbience;
    public Transform armature;
    public Animation anim;
    public int health = 10;
    public int stop, raycasthit;
    public bool animstart, inaction, started, sprint, sprintEnabled, moving, triggerEntered, triggerEnteredFree, idleblock;
    public AudioSource dooropen, footstep;
    public List<AudioSource> hurt;
    public List<AudioClip> footsteps;
    public Vector3 direction, position;
    public LayerMask building, surfaces;
    void Start(){
        if(rotationSpeed == 0){
            rotationSpeed = 2.8f;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        direction = new Vector3(0.1f, 0f, 0.1f).normalized;
        me.Move((torso.transform.forward+=Physics.gravity) * speed * Time.deltaTime);
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
                stop = 0;
                if (sprint == false)
                {
                    if (!anim.IsPlaying("walk"))
                    {
                        anim.CrossFade("walk", 0.1f);
                        anim["walktorso"].layer = 1;
                        anim.CrossFade("walktorso", 0.1f);
                    }
                }
                if(sprintEnabled){
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (!anim.IsPlaying("run"))
                        {
                            anim["run"].speed = 0.7f;
                            anim.CrossFade("run", 0.1f);
                            speed = sprintspeed;
                            anim["runtorso"].speed = 0.7f;
                            anim["runtorso"].layer = 1;
                            anim.CrossFade("runtorso", 0.1f);
                        }
                            sprint = true;
                    }
                    else
                    {
                        speed = 1.2f;
                        sprint = false;
                    }
                }
                moving = true;
                Vector3 forward = torso.transform.forward;
                Vector3 backward = -torso.transform.forward;
                Vector3 right = torso.transform.right;
                Vector3 left = -torso.transform.right;
                if(Input.GetKey(KeyCode.W)){
                    me.Move((backward+=Physics.gravity) * speed * Time.deltaTime);
                    idleblock = false;
                    //anim["walk"].speed = 1;
                }
                if(Input.GetKey(KeyCode.S)){
                    me.Move((forward+=Physics.gravity) * speed * Time.deltaTime);
                    idleblock = false;
                    //anim["walk"].speed = -1;
                    //anim["walk"].time = anim["walk"].length; doesnt work raaaa
                }
            }
            else
            {
                moving = false;
                if(moving == false){
                    if(!anim.IsPlaying("idle") && !idleblock){
                        anim.Stop();
                        anim.Play("idle");
                    }
                }
            }
        }
    }
    void Update(){
        if(!inaction){
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 1f, building))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(dooropen != null){
                        dooropen.Play();
                    }
                    fadeout.SetActive(true);
                    GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = hit.transform.name;
                    GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
                }
            }
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2f, surfaces))
            {
                Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.gameObject.name == "Sand"){
                    if(footstep.clip != footsteps[0]){
                        footstep.clip = footsteps[0];
                    }
                }
                if(hit.transform.gameObject.name == "Wood"){
                    if(footstep.clip != footsteps[1]){
                        footstep.clip = footsteps[1];
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.name == "Freelook"){
            if(triggerEnteredFree){
                Debug.Log("triggerNotEntered");
                triggerEnteredFree = false;
                freelook.SetActive(true);
                oceanAmbience.SetActive(false);
                light.SetActive(false);
                cam1.SetActive(false);
                cam2.SetActive(false);
            }else{
                Debug.Log("triggerEntered");
                triggerEnteredFree = true;
                freelook.SetActive(false);
                oceanAmbience.SetActive(true);
                light.SetActive(true);
                cam1.SetActive(true);
                cam2.SetActive(false);
            }
        }
        if(other.transform.gameObject.name == "Cam2"){
            if(!triggerEntered){
                triggerEntered = true;
                light.SetActive(true);
                cam1.SetActive(false);
                cam2.SetActive(true);
                freelook.SetActive(false);
            }else{
                triggerEntered = false;
                light.SetActive(true);
                cam1.SetActive(true);
                cam2.SetActive(false);
                freelook.SetActive(false);
            }
        }
        if(other.transform.gameObject.name == "Cam2Fix"){
            triggerEntered = true;
            light.SetActive(true);
            cam1.SetActive(false);
            cam2.SetActive(true);
            oceanAmbience.SetActive(true);
            freelook.SetActive(false);
        }
        if(other.transform.gameObject.name == "transition"){
            dooropen.Play();
            fadeout.SetActive(true);
            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
        }
    }
    public void damage(){
        StartCoroutine(injured());
    }
    IEnumerator injured(){
        inaction = true;
        anim["injured"].speed = 1.5f;
        anim.Play("injured");
        hurt[Random.Range(0, hurt.Count)].Play();
        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
        if(PlayerPrefs.GetInt("Health") <= 0){
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene("Death");
        }
        yield return new WaitForSeconds(1.5f);
        inaction = false;
    }
}