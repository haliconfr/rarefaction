using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLights : MonoBehaviour
{
    public GameObject video;
    public GameObject videocamera;
    public GameObject maincamera;
    public GameObject canvas;
    void Start()
    {
        StartCoroutine(videoend());
    }
    IEnumerator videoend()
    {
        yield return new WaitForSeconds(12);
        canvas.SetActive(true);
        videocamera.SetActive(false);
        maincamera.SetActive(true);
        Destroy(this.gameObject);
    }
}