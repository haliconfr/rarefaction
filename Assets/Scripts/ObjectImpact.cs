using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectImpact : MonoBehaviour
{
    public AudioSource impact;
    void OnCollisionEnter(Collision collision)
    {
        impact.Play();
    }
}