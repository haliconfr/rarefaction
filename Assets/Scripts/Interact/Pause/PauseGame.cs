using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    bool open;
    public GameObject menu;
    GameObject character, fade;
    void Start(){
        character = GameObject.Find("Character");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!open){
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                fade = GameObject.Find("Fade");
                fade.SetActive(false);
                character.GetComponent<ThirdPersonController>().inaction = true;
                menu.SetActive(true);
                open = true;
            }else{
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                fade.SetActive(true);
                character.GetComponent<ThirdPersonController>().inaction = false;
                menu.SetActive(false);
                open = false;
            }
        }
    }
}
