using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    void Start()
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.AddForce(150, 0, 0);
    }
}