using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    int selection;
    public GameObject gunmenu;
    public AudioSource changeSnd, selectSnd, errorSnd, pauseMus, reloadSnd;
    public Text ammo;
    public int frames;
    bool played, selected;
    public Animator box;

    void Start(){
        gunmenu.SetActive(false);
        ammo.text = "loaded: " + PlayerPrefs.GetInt("AmmoLoaded", 0) + "\nammo:" + PlayerPrefs.GetInt("Ammo", 0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(selection != 1){
                selection = 1;
                selected = false;
                changeSnd.Play();
                box.SetBool("Health", true);
                box.SetBool("Gun", false);
                gunmenu.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(selection != 2){
                selection = 2;
                selected = false;
                changeSnd.Play();
                box.SetBool("Gun", true);
                box.SetBool("Health", false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            if(!selected){
                select();
            }
        }
        if(!played){
            if(frames == 1999){
                pauseMus.Play();
                played = true;
            }else{
                frames++;
            }
        }
    }
    void select()
    {
        switch(selection)
        {
            case 1:
                errorSnd.Play();
                box.SetBool("Gun", false);
                box.SetBool("Health", true);
                break;
            case 2:
                selectSnd.Play();
                gunmenu.SetActive(true);
                selected = true;
                break;
        }
    }
    public void reload(){
        Debug.Log("pressed");
        reloadSnd.Play();
        int loaded = PlayerPrefs.GetInt("AmmoLoaded", 0);
        int owned = PlayerPrefs.GetInt("Ammo", 0);
        int diff = 15 - loaded;
        diff = Mathf.Min(diff, owned);
        loaded += diff;
        owned -= diff;
        PlayerPrefs.SetInt("AmmoLoaded", loaded);
        PlayerPrefs.SetInt("Ammo", owned);
        ammo.text = "loaded: " + PlayerPrefs.GetInt("AmmoLoaded", 0) + "\nammo:" + PlayerPrefs.GetInt("Ammo", 0);
        gunmenu.SetActive(false);
    }
}