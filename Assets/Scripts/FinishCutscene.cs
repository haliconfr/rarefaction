using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCutscene : MonoBehaviour
{
    public float time;
    public string nextscene;
    void Start()
    {
        StartCoroutine(videoend());
    }
    IEnumerator videoend()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nextscene);
    }
}