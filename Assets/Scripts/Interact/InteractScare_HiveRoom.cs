using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScare_HiveRoom : MonoBehaviour
{
    public GameObject fastBees, oldBees;
    void OnTriggerEnter(Collider collider)
    {
        oldBees.SetActive(false);
        fastBees.SetActive(true);
    }
}