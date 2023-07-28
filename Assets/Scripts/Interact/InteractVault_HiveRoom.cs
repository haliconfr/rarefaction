using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractVault_HiveRoom : MonoBehaviour
{
    public LayerMask vault, everything;
    ThirdPersonControllerHarbour tpc;
    public Animation armAnim;
    public Transform armature;
    public GameObject fallingHive;
    public string death;
    void Start(){
        if(SceneManager.GetActiveScene().name == "HiveRoom"){
            PlayerPrefs.SetString("SaveRoom", "HiveRoom");
        }
        PlayerPrefs.Save();
    }
    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward), Color.green, 0.7f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, vault))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(hit.transform.gameObject.name == "scissors"){
                    PlayerPrefs.SetString("SaveRoom", "HiveRoom2");
                    Debug.Log(PlayerPrefs.GetString("SaveRoom"));
                    PlayerPrefs.SetInt("GotScissors", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("HiveRoom2");
                }else{
                    Vector3 newPos = new Vector3(transform.position.x, hit.transform.GetChild(0).transform.position.y, hit.transform.GetChild(0).transform.position.z);
                    transform.position = newPos;
                    tpc = GetComponent<ThirdPersonControllerHarbour>();
                    tpc.inaction = true;
                    armAnim.Play("hiveroomtop");
                    StartCoroutine(postanim());
                    hit.transform.gameObject.layer = everything;
                }
            }
        }
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Obstacle"){
            PlayerPrefs.SetString("COD", "bees");
            SceneManager.LoadScene("Death");
        }
        if(collider.gameObject.name == "hiveFall"){
            fallingHive.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    IEnumerator postanim(){
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = armature.position;
        armature.transform.localPosition = new Vector3(0,0,0);
        armature.transform.rotation = gameObject.transform.rotation;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("finished anim");
        tpc.inaction = false;
    }
}