using UnityEngine;
using System;

public class AudioTimeHandler : MonoBehaviour
{
    public static event Action OnBeat; // anything can subscribe
    public double bpm = 120.0;
    private double beatInterval;
    private double nextBeatDspTime;

    void Start()
    {
        beatInterval = 60.0 / bpm;
        nextBeatDspTime = AudioSettings.dspTime + beatInterval;
    }

    void Update()
    {
        double dsp = AudioSettings.dspTime;

        if (dsp >= nextBeatDspTime)
        {
            OnBeat?.Invoke();
            nextBeatDspTime += beatInterval; // schedule the next beat
        }
    }
}