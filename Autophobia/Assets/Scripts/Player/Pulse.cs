using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pulse : MonoBehaviour
{
    public ModularAudioHandler M;
    private Vector3 StartScale = new Vector3(16f,16f,16f);
    private Vector3 MaxScale = new Vector3(23f,23f,23f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine (Process());
        }
    }
    IEnumerator Process()
    {
        transform.localScale = Vector3.Lerp (StartScale, MaxScale, 0.2f);
        yield return new WaitForSeconds (0.2f) ;
        transform.localScale = Vector3.Lerp (MaxScale, StartScale, 0.2f);
    }
}
