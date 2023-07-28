using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSfx : MonoBehaviour
{
    public AudioSource walk;
    void Sound()
    {
        walk.Play();
    }
}