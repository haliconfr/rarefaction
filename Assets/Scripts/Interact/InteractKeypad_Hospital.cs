using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractKeypad_Hospital : MonoBehaviour
{
    public string input;
    public AudioSource wrongSnd, rightSnd, thinking, press, dooropen;
    public Sprite wrongSpr, rightSpr, nolight;
    public GameObject background, character, keypad, fadeout;

    void OnEnable(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        character.GetComponent<ThirdPersonController>().inaction = true;
        character.GetComponent<useweapons>().enabled = false;
        background.SetActive(true);
    }
    public void onClick(string number)
    {
        input = input + number;
        press.Play();
        if(input.Length == 4){
            if(input == "2714"){
                StartCoroutine(right());
            }else{
                StartCoroutine(wrong());
            }
        }
    }
    IEnumerator right(){
        thinking.Play();
        yield return new WaitForSeconds(2);
        rightSnd.Play();
        keypad.GetComponent<Image>().sprite = rightSpr;
        input = "";
        yield return new WaitForSeconds(2f);
        dooropen.Play();
        fadeout.SetActive(true);
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = "Harbour";
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
        //TODO make the door to the hut functional
    }
    IEnumerator wrong(){
        thinking.Play();
        yield return new WaitForSeconds(2);
        wrongSnd.Play();
        input = "";
        keypad.GetComponent<Image>().sprite = wrongSpr;
        yield return new WaitForSeconds(1);
        keypad.GetComponent<Image>().sprite = nolight;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            character.GetComponent<useweapons>().enabled = true;
            character.GetComponent<ThirdPersonController>().inaction = false;
            character.GetComponent<ThirdPersonController>().lockedtext.SetActive(false);
            background.SetActive(false);
            input = "";
            keypad.SetActive(false);
        }
    }
}