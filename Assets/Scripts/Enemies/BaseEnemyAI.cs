using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BaseEnemyAI : MonoBehaviour
{
    public Transform player;
    public GameObject playerObj;
    NavMeshAgent agent;
    public List<Transform> idlepos;
    public List<AudioSource> sounds;
    public int health;
    public Animator monsterAnimator;
    public List<AudioSource> impact;
    public ParticleSystem particleSystem;
    bool idling;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "LobbyNoLight"){
            PlayerPrefs.SetString("SaveRoom", "LobbyNoLight");
        }
        //might not work coz the raycast thing is different
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(idlesounds());
    }

    void FixedUpdate()
    {
        if(player.gameObject.active == true){
            agent.SetDestination(player.position);
        }
        //if(!idling){
        //    StartCoroutine(idlepositions());
        //}
    }
    public void FoundCharacter(){
        Vector3 target = new Vector3(player.position.x, this.transform.position.y, player.position.z) ;
        this.transform.LookAt(target) ;
        Debug.Log("found character");
        StopCoroutine(idlepositions());
        idling = false;
        agent.SetDestination(transform.position);
        if(!monsterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Mon_BaseAttack")){
            monsterAnimator.Play("Mon_BaseAttack");
            StartCoroutine(hurtplayer());
        }
    }
    public void hit(){
        health--;
        particleSystem.Play();
        impact[Random.Range(0, impact.Count)].Play();
        if(health == 0){
            Destroy(this.gameObject);
        }
    }
    IEnumerator idlepositions(){
        idling = true;
        agent.SetDestination(idlepos[Random.Range(0, idlepos.Count)].position);
        yield return new WaitForSeconds(10);
        StartCoroutine(idlepositions());
    }
    IEnumerator idlesounds(){
        yield return new WaitForSeconds(Random.Range(5, 15));
        sounds[Random.Range(0, sounds.Count)].Play();
        StartCoroutine(idlesounds());
    }
    IEnumerator hurtplayer(){
        yield return new WaitForSeconds(0.7f);
        if(SceneManager.GetActiveScene().name == "LobbyNoLight"){
            playerObj.GetComponent<ThirdPersonController>().damage();
        }else{
            playerObj.GetComponent<ThirdPersonControllerHarbour>().damage();
        }
    }
}