using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameHarbour : MonoBehaviour
{
    bool open;
    public GameObject menu;
    GameObject character, fade, mCamera;
    void Start(){
        character = GameObject.Find("Character");
        mCamera = Camera.main.gameObject;
        if(SceneManager.GetActiveScene().name == "HiveRoom"){
            mCamera = GameObject.Find("Camera");
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!open){
                if(mCamera != null){
                    
                }
                mCamera.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                fade = GameObject.Find("Fade");
                fade.SetActive(false);
                character.GetComponent<ThirdPersonControllerHarbour>().inaction = true;
                menu.SetActive(true);
                open = true;
            }else{
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                fade.SetActive(true);
                character.GetComponent<ThirdPersonControllerHarbour>().inaction = false;
                menu.SetActive(false);
                open = false;
                mCamera.SetActive(true);
            }
        }
    }
}