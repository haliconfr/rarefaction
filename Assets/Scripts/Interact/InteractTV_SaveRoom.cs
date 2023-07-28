using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTV_SaveRoom : MonoBehaviour
{
    public GameObject SaveScreen;
    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f))
        {
            if(hit.transform.name == "TV"){
                if(Input.GetKeyDown(KeyCode.E)){
                    SaveScreen.SetActive(true);
                    GameObject RoomReturn = GameObject.Find("RoomReturn");
                    PlayerPrefs.SetString("SaveRoom", RoomReturn.GetComponent<RoomReturn>().originalLevel);
                }
            }
        }
    }
}