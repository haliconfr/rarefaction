using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class variables : MonoBehaviour
{
    public float sensitivity, deagleammoloaded, deagleammoowned;
    public string weaponholding, keypadinput;
    public GameObject deagle;
    public int shotsfired;
    public void Start()
    {
        weaponholding = "deagle";
    }
}
