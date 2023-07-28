using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPiston_Hospital : MonoBehaviour
{
    public GameObject textbox;
    public LayerMask interact;
    public bool active;
    void Start(){
        PlayerPrefs.SetString("SaveRoom", "Piston");
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, interact))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                textbox.SetActive(true);
                StartCoroutine(text());
            }
        }
    }
    IEnumerator text(){
        yield return new WaitForSeconds(3);
        textbox.SetActive(false);
    }
}