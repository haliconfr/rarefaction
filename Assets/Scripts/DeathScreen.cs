using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    string COD;
    public AudioSource bees, normDeath;
    public GameObject canvas;
    bool active;
    void Start()
    {
        StartCoroutine(timer());
        if(GameObject.Find("Character") != null){
            COD = GameObject.Find("Character").GetComponent<InteractVault_HiveRoom>().death;
            Destroy(GameObject.Find("Character"));
            if(PlayerPrefs.GetString("COD") == "bees"){
                bees.Play();
            }else{
                normDeath.Play();
            }
        }else{
            normDeath.Play();
        }
        PlayerPrefs.SetString("COD", "");
    }
    void Update(){
        if(Input.anyKey && active){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bees.Stop();
            normDeath.Stop();
            canvas.SetActive(true);
        }
    }
    IEnumerator timer(){
        yield return new WaitForSeconds(1f);
        active = true;
    }
    public void continu(){
        PlayerPrefs.SetInt("Health", 5);
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = PlayerPrefs.GetString("SaveRoom");
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
    }
    public void quit(){
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = "Menu";
        GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
    }
}