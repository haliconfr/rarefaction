using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalFollow : MonoBehaviour
{
    public Transform player;
    float y;
    public float x;
    
    void Start(){
        y = transform.position.y;
        //z = transform.position.z;
    }

    void Update()
    {
        if(player != null){
            transform.position = new Vector3(player.position.x + x, y, player.position.z);
        }
    }
}
