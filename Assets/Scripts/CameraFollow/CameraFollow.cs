using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    float y;
    public float z;
    
    void Start(){
        y = transform.position.y;
        //z = transform.position.z;
    }

    void Update()
    {
        if(player != null){
            transform.position = new Vector3(player.position.x, y, player.position.z + z);
        }
    }
}
