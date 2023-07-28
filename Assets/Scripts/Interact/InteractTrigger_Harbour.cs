using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger_Harbour : MonoBehaviour
{
    public GameObject newBoat, fadeout, fadeIn, newCharacter, newCamera;
    public GameObject oldBoat, oldcamera;
    void OnTriggerEnter()
    {
        StartCoroutine(fade());
    }
    IEnumerator fade(){
        fadeout.SetActive(true);
        fadeIn.SetActive(false);
        yield return new WaitForSeconds(1);
        newBoat.SetActive(true);
        newCharacter.SetActive(true);
        oldcamera.SetActive(false);
        newCamera.SetActive(true);
        fadeIn.SetActive(true);
        fadeout.SetActive(false);
        oldBoat.SetActive(false);
    }
}