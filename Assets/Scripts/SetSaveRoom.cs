using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSaveRoom : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("SaveRoom", SceneManager.GetActiveScene().name);
    }
}