using   UnityEngine;
using   System;

public class ModularAudioHandler : MonoBehaviour
{
    public AudioSource      music;
    public double           bpm;
    public int              beatspermeasure;

    public static event     Action onEbeat;
    public static event     Action onQbeat;
    public static event     Action onHbeat;
    public static event     Action onbeat;
    public static event     Action on2beats;
    public static event     Action on3beats;
    public static event     Action on4beats;
    public static event     Action onmeasure;

    public double           dspstart;


    public double           Ebeatint;
    public double           Qbeatint;
    public double           Hbeatint;    
    public double           beatint;
    public double           beat2int;
    public double           beat3int;
    public double           beat4int;
    public double           measureint;

    void Awake()
    {
        beatint     = 60.0 / bpm;
        Ebeatint    = beatint / 8.0;
        Qbeatint    = beatint / 4.0;
        Hbeatint    = beatint / 2.0;
        beat2int    = beatint * 2.0;
        beat3int    = beatint * 3.0;
        beat4int    = beatint * 4.0;
        measureint  = beatint * beatspermeasure;
    }

}
