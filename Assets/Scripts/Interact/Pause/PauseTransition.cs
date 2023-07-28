using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTransition : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Options;
    
    public void InventoryClick()
    {
        Inventory.SetActive(true);
        Options.SetActive(false);
    }
    public void OptionsClick()
    {
        Inventory.SetActive(false);
        Options.SetActive(true);
    }
}