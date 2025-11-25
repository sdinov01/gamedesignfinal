using UnityEngine;
using System;

public class BeatSync : MonoBehaviour
{
    public static event Action OnBeat;          // Calls every beat (or quarter-measure in 4/4)
    public static event Action OnHalfBeat;      // Calls every two beats (or half-measure in 4/4)
    public static event Action OnMeasure;       // Calls every measure

    public AudioSource music;                   
    public double bpm;                          
    public int beatsPerMeasure;                 

    private double dspStartTime;
    private double beatInterval;         
    private double halfBeatInterval;     
    private double nextBeatTime;         
    private double nextHalfBeatTime;     
    private int beatCountInMeasure = 0;  

    void Start()
    {
        beatInterval = 60.0 / bpm;
        halfBeatInterval = beatInterval / 2.0;

        // Schedule according to when the AudioSource actually starts playing
        dspStartTime = AudioSettings.dspTime;
        music.PlayScheduled(dspStartTime);

        nextBeatTime = dspStartTime + beatInterval;
        nextHalfBeatTime = dspStartTime + halfBeatInterval;
    }

    void Update()
    {
        double dsp = AudioSettings.dspTime;

        // ---- Half Beat ----
        if (dsp >= nextHalfBeatTime)
        {
            OnHalfBeat?.Invoke();
            nextHalfBeatTime += halfBeatInterval;
        }

        // ---- Beat ----
        if (dsp >= nextBeatTime)
        {
            OnBeat?.Invoke();

            beatCountInMeasure++;

            // ---- Measure ----
            if (beatCountInMeasure >= beatsPerMeasure)
            {
                OnMeasure?.Invoke();
                beatCountInMeasure = 0;
            }

            nextBeatTime += beatInterval;
        }
    }
}
