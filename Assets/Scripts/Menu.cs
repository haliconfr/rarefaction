using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu, controls, menuFadeout, controlsFadeout, fade;
    public Text play;
    public void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(PlayerPrefs.GetString("SaveRoom") != string.Empty){
            play.text = "continue";
        }
    }
    public void Play(){
        fade.SetActive(true);
        if(PlayerPrefs.GetString("SaveRoom") == string.Empty){
            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = "OpeningCutscene";
            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
        }else{
            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().levelname = PlayerPrefs.GetString("SaveRoom");
            GameObject.Find("LoadLev").GetComponent<LoadNextLevel>().invoked();
        }
    }
    public void delete(){
        PlayerPrefs.DeleteAll();
    }
    public void cntrl()
    {
        StartCoroutine(fadeout(menuFadeout, controls, menu));
    }

    IEnumerator fadeout(GameObject fade, GameObject newCanvas, GameObject oldCanvas){
        fade.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        oldCanvas.SetActive(false);
        newCanvas.SetActive(true);
        fade.SetActive(false);
    }

    public void main()
    {
        StartCoroutine(fadeout(controlsFadeout, menu, controls));
    }
    public void Quit(){
        Application.Quit(0);
    }
}
