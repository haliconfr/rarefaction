using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractGenerator_Hospital : MonoBehaviour
{
    public GameObject fadeout;
    public LayerMask interact;
    void Start(){
        PlayerPrefs.SetString("SaveRoom", "Generator");
    }
    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, interact))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(transition());
            }
        }
    }
    IEnumerator transition(){
        fadeout.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GeneratorNoLight");
        PlayerPrefs.SetInt("NoLight", 1);
    }
}