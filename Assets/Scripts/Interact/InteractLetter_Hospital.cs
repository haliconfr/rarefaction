using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractLetter_Hospital : MonoBehaviour
{
    public LayerMask interact;
    bool interacted;
    public GameObject textbox;
    int line;
    public List<string> lines;
    ThirdPersonController tpc;
    public AudioSource skip;
    void Start(){
        PlayerPrefs.SetString("SaveRoom", "Room");
    }

    void LateUpdate()
    {
        if(!interacted){
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, interact))
            {
                if(Input.GetKeyDown(KeyCode.E)){
                    interacted = true;
                    tpc = this.gameObject.GetComponent<ThirdPersonController>();
                    tpc.inaction = true;
                    textbox.SetActive(true);
                    text();
                }
            }
        }else{
            if(Input.GetKeyDown(KeyCode.E)){
                line++;
                if(line < lines.Count){
                    text();
                }else{
                    interacted = false;
                    tpc = this.gameObject.GetComponent<ThirdPersonController>();
                    tpc.inaction = false;
                    textbox.SetActive(false);
                    line = 0;
                }
            }
        }
    }
    void text(){
        if(skip != null){
            skip.Play();
        }
        textbox.GetComponent<Text>().text = lines[line];
    }
}