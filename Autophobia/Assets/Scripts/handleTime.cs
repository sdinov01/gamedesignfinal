using UnityEngine;
using System;
using System.Collections;


public class handleTime : MonoBehaviour
{
    public float tempo;
    public int beatspermeasure;

    public static event Action OnFrameInterval;

    private int beat;
    private bool onbeat;
    private int measure;

    void Start()
    {
        // SpawnCircle();
        beat = 0;
        measure = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int beatframe = (int)(tempo * 16);
        onbeat = (Time.frameCount % beatframe == 0);
        measure = Time.frameCount / (beatframe * 4);

    }
}
