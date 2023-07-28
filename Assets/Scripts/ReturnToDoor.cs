using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToDoor : MonoBehaviour
{
    public List<string> scenes;
    public List<Transform> positions;
    public Transform character;
    int position;
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("SaveRoom"));
        if(SceneManager.GetActiveScene().name == "Lobby" && PlayerPrefs.GetInt("EnteredHospital") == 1){
            GameObject.Find("CameraTrigger").GetComponent<CameraTransition>().switchcam();
        }
        if(scenes.Contains(PlayerPrefs.GetString("SaveRoom"))){
            position = scenes.FindIndex(isName);
            character.position = positions[position].position;
            character.rotation = positions[position].rotation;
        }
    }
    private bool isName(string name)
    {
        return (name==PlayerPrefs.GetString("SaveRoom"));
    }
}