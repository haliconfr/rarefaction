using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretSound : MonoBehaviour
{
    public AudioSource sound;
    bool played;
    void OnEnable()
    {
        if(!played){
            StartCoroutine(wait());
        }
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(8f);
        sound.Play();
        played = true;
    }
}