using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBoat_Harbour : MonoBehaviour
{
    public LayerMask boat;
    public GameObject fadeout, fadein, text, mc, boatcam;
    bool switched;
    void LateUpdate()
    {
        //TODO options menu volume
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, boat))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(PlayerPrefs.GetInt("GotScissors", 0) == 0){
                    if(!switched){
                        switched = true;
                    }else{
                        switched = false;
                    }
                    StartCoroutine(transition());
                }else{
                    fadeout.SetActive(true);
                    GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = "MansionExterior";
                    GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
                }
            }
        }
    }
    IEnumerator transition(){
        if(switched){
            mc.GetComponent<ThirdPersonControllerHarbour>().inaction = true;
            fadeout.SetActive(true);
            fadein.SetActive(false);
            yield return new WaitForSeconds(2);
            fadeout.SetActive(false);
            boatcam.SetActive(true);
            text.SetActive(true);
            fadein.SetActive(true);
        }else{
            mc.GetComponent<ThirdPersonControllerHarbour>().inaction = false;
            fadeout.SetActive(true);
            fadein.SetActive(false);
            yield return new WaitForSeconds(2);
            fadeout.SetActive(false);
            boatcam.SetActive(false);
            text.SetActive(false);
            fadein.SetActive(true);
        }
    }
}