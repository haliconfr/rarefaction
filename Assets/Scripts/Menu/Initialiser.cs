using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialiser : MonoBehaviour
{
    public GameObject text, fade;
    public bool active;
    void Start(){
        StartCoroutine(splash());
    }
    IEnumerator splash()
    {
        yield return new WaitForSeconds(6f);
        text.SetActive(true);
        active = true;
    }
    void LateUpdate(){
        if(active){
            if(Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(fadeout());
            }
        }
    }
    IEnumerator fadeout()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}
