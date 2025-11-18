using UnityEngine;
using System;

public class BeatSync : MonoBehaviour
{
    public static event Action OnBeat;      // every beat
    public static event Action OnHalfBeat;  // twice per beat
    public static event Action OnMeasure;   // every 4 beats

    public AudioSource music;       // assign in Inspector
    public double bpm;
    public int beatsPerMeasure; // 4/4 time

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
