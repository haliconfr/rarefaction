using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useweapons : MonoBehaviour
{
    public GameObject deagle;
    GameObject character;
    void Start(){
        character = this.gameObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(character.GetComponent<ThirdPersonController>() != null && character.GetComponent<ThirdPersonController>().inaction == false || character.GetComponent<ThirdPersonControllerHarbour>() != null && character.GetComponent<ThirdPersonControllerHarbour>().inaction == false){
                deagle.GetComponent<fire>().shoot();
            }
        }
    }
}
