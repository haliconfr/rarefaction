using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMonsters : MonoBehaviour
{
    public LayerMask monster;
    void LateUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, monster))
        {
            hit.transform.gameObject.GetComponent<BaseEnemyAI>().FoundCharacter();
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 0.5f, monster))
        {
            hit.transform.gameObject.GetComponent<BaseEnemyAI>().FoundCharacter();
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.5f, monster))
        {
            hit.transform.gameObject.GetComponent<BaseEnemyAI>().FoundCharacter();
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.5f, monster))
        {
            hit.transform.gameObject.GetComponent<BaseEnemyAI>().FoundCharacter();
        }
    }
}