using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomReturn : MonoBehaviour
{
    public string originalLevel;
    public Vector3 position;
    void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == originalLevel){
            if(originalLevel == "Lobby"){
                GameObject.Find("CameraTrigger").GetComponent<CameraTransition>().Camera.SetActive(false);
                GameObject.Find("CameraTrigger").GetComponent<CameraTransition>().Camera1.SetActive(true);
                GameObject.Find("CameraTrigger").GetComponent<CameraTransition>().switched = true;
            }
            Destroy(this.gameObject);
        }
    }
}
