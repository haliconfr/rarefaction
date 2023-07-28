using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController me;
    public GameObject gun, torso, fadeout, lockedtext, keypad;
    public float speed, sprintspeed, rotationSpeed;
    public Animation anim;
    public int health = 10;
    public int stop, raycasthit;
    public string exitunlocked;
    public bool animstart, inaction, started, sprint, sprintEnabled, moving, exitused, idleblock;
    public AudioSource dooropen, footstep;
    public List<AudioSource> hurt;
    public List<AudioClip> footsteps;
    public Vector3 direction, position;
    public LayerMask building, surfaces;
    void Start(){
        if(SceneManager.GetActiveScene().name == "Room Empty"){
            PlayerPrefs.SetString("SaveRoom", "Room Empty");
        }
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
            transform.Rotate((transform.up * horizontal)/rotationSpeed);
            if (direction.magnitude >= 0.1f)
            {
                stop = 0;
                if (sprint == false)
                {
                    if (!anim.IsPlaying("walk"))
                    {
                        anim.CrossFade("walk", 0.2f);
                        anim["walktorso"].layer = 1;
                        anim.CrossFade("walktorso", 0.2f);
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
                    idleblock = false;
                    me.Move((backward+=Physics.gravity) * speed * Time.deltaTime);
                }
                if(Input.GetKey(KeyCode.S)){
                    idleblock = false;
                    me.Move((forward+=Physics.gravity) * speed * Time.deltaTime);
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
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2f, surfaces))
            {
                if(hit.transform.gameObject.name == "Concrete"){
                    if(footstep.clip != footsteps[0]){
                        footstep.clip = footsteps[0];
                    }
                }
                if(hit.transform.gameObject.name == "Metal"){
                    if(footstep.clip != footsteps[1]){
                        footstep.clip = footsteps[1];
                    }
                }
                if(hit.transform.gameObject.name == "Dirt"){
                    if(footstep.clip != footsteps[1]){
                        footstep.clip = footsteps[1];
                    }
                }
            }
            if(Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.forward), out hit, 1f, building))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(hit.transform.gameObject.name == "Generator" && PlayerPrefs.GetInt("NoLight", 0) == 1){
                        raycasthit = 0;
                    }
                    if(hit.transform.gameObject.name == "Room Empty" && PlayerPrefs.GetInt("NoLight", 0) == 1){
                        raycasthit = 0;
                    }
                    if(hit.transform.gameObject.name == "Locked"){
                        raycasthit = 1;
                    }
                    if(hit.transform.gameObject.name == "Exit"){
                        if(PlayerPrefs.GetInt("GotHospitalKey", 0) == 1){
                            if(exitused){
                                lockedtext.SetActive(false);
                                keypad.SetActive(true);
                            }else{
                                raycasthit = 2;
                            }
                        }else{
                            raycasthit = 1;
                        }
                    }
                    switch(raycasthit){
                        case 0:
                            dooropen.Play();
                            fadeout.SetActive(true);
                            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = hit.transform.name;
                            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
                            break;
                        case 1:
                            lockedtext.SetActive(true);
                            StartCoroutine(textdisappear());
                            raycasthit = 0;
                            break;
                        case 2:
                            Debug.Log("exit");
                            lockedtext.SetActive(true);
                            lockedtext.GetComponent<Text>().text = exitunlocked;
                            exitused = true;
                            break;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
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
            SceneManager.LoadScene("Death");
        }
        yield return new WaitForSeconds(1.5f);
        inaction = false;
    }
    IEnumerator textdisappear(){
        yield return new WaitForSeconds(2);
        lockedtext.SetActive(false);
    }
}