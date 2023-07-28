using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractAmmoPickup : MonoBehaviour
{
    public GameObject notice;
    public LayerMask layer;
    public GameObject mc;
    bool opened, lookingAt;
    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward), Color.green, 0.7f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.7f, layer))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(!opened){
                    PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo", 0) + 5);
                    mc.GetComponent<ThirdPersonController>().inaction = true;
                    notice.SetActive(true);
                    opened = true;
                }else{
                    Destroy(hit.transform.gameObject);
                    mc.GetComponent<ThirdPersonController>().inaction = false;
                    notice.SetActive(false);
                }
            }
        }
    }
}