using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public string levelname;
    public GameObject roomReturn;
    void Update(){
        if(SceneManager.GetActiveScene().name == "Loading"){
            GameObject loader = GameObject.Find("Loader");
            loader.GetComponent<Loader>().LoadLevel(levelname);
            Destroy(this.gameObject);
        }
    }
    public void invoked(){
        StartCoroutine(wait());
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(2f);
        DontDestroyOnLoad(this.gameObject);
        if(roomReturn != null){
            roomReturn.SetActive(true);
        }
        SceneManager.LoadScene("Loading");
    }
}