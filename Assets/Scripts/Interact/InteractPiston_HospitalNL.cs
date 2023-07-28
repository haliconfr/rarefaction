using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPiston_HospitalNL : MonoBehaviour
{
    public GameObject textbox, character;
    public LayerMask interact;
    public string line, afterline;
    void Start(){
        PlayerPrefs.SetString("SaveRoom", "PistonNoLight");
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, interact))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(PlayerPrefs.GetInt("GotHospitalKey", 0) != 1){
                    textbox.GetComponent<Text>().text = line;
                    textbox.SetActive(true);
                }else{
                    if(textbox.active == false){
                        textbox.GetComponent<Text>().text = afterline;
                        textbox.SetActive(true);
                        character.GetComponent<ThirdPersonController>().inaction = true;
                    }else{
                        character.GetComponent<ThirdPersonController>().inaction = false;
                        textbox.SetActive(false);
                    }
                }
                PlayerPrefs.SetInt("GotHospitalKey", 1);
            }
        }
    }
}