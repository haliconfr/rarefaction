using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractNotes : MonoBehaviour
{
    public LayerMask interact;
    bool interacted;
    public GameObject fadeIn, fadeOut, note;
    ThirdPersonControllerHarbour tpc;
    void LateUpdate()
    {
        if(!interacted){
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, interact))
            {
                if(Input.GetKeyDown(KeyCode.E)){
                    if(!interacted){
                        interacted = true;
                        if(SceneManager.GetActiveScene().name == "LobbyNoLight"){
                            ThirdPersonController tpcontroller = this.gameObject.GetComponent<ThirdPersonController>();
                            tpcontroller.inaction = true;
                        }else{
                            tpc = this.gameObject.GetComponent<ThirdPersonControllerHarbour>();
                            tpc.inaction = true;
                        }
                        StartCoroutine(noteAppear());
                    }
                }
            }
        }else{
            if(Input.GetKeyDown(KeyCode.E)){
                interacted = false;
                if(SceneManager.GetActiveScene().name == "LobbyNoLight"){
                    ThirdPersonController tpcontroller = this.gameObject.GetComponent<ThirdPersonController>();
                    tpcontroller.inaction = false;
                }else{
                    tpc.inaction = false;
                }
                StartCoroutine(noteDissapear());
            }
        }
    }
    IEnumerator noteAppear(){
        fadeOut.SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeOut.SetActive(false);
        fadeIn.SetActive(false);
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        note.SetActive(true);
    }
    IEnumerator noteDissapear(){
        fadeIn.SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeOut.SetActive(false);
        fadeIn.SetActive(true);
        note.SetActive(false);
    }
}