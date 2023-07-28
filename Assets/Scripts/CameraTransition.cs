using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Camera1;
    public Canvas canvas;
    public bool switched;
    void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("EnteredHospital", 1);
        switchcam();
    }
    public void switchcam(){
        if(switched){
            Camera.SetActive(true);
            Camera1.SetActive(false);
            canvas.worldCamera = Camera.GetComponent<Camera>();
            switched = false;
        }else{
            Camera.SetActive(false);
            Camera1.SetActive(true);
            canvas.worldCamera = Camera1.GetComponent<Camera>();
            switched = true;
        }
    }
}