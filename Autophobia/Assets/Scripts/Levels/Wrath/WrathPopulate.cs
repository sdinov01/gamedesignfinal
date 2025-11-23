using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrathPopulate : MonoBehaviour
{
    public AudioSource              music;
    public int                      measureindex;      
    public double                   mInterval;
    public ModularAudioHandler      audioparser;    
    public RhythmManager            RM;
    public TutorialManager          TM;


    private double                  nextM;
    private double                  audiostarttime;
    public GameObject               hand;
    public double dsp;
    private bool s2;

    void Start()
    {
        audioparser     = GetComponent<ModularAudioHandler>();
        RM              = GetComponent<RhythmManager>();
        mInterval       = audioparser.measureint;

        audiostarttime  = AudioSettings.dspTime;
        nextM           = audiostarttime + mInterval;
        measureindex    = 0;
        TM.startup = true;
        TM.step2 = true;
        // StartCoroutine (TM.WaitForSeconds ((float)mInterval));
        music.Play();
        RM.TriggerNextKnife();
        TM.startup = false;
    }

    // Update is called once per frame
    void Update()
    {
        dsp = AudioSettings.dspTime;
        if (dsp >= nextM)
        {
            measureindex++;

            switch (measureindex) {
                case 2: MType2(); break;
                case 5: MType3(); break;

                default: break;
            }


            nextM += mInterval;
        }
    }

    void MType1()
    {
        RM.TriggerNextKnife();
    }

    void MType2()
    {
        StartCoroutine(WaitHalfMeasure());
        RM.TriggerNextKnife();
    }

    IEnumerator WaitHalfMeasure()
    {
        yield return new WaitForSeconds ((float)audioparser.beat2int);
    }

    IEnumerator WaitThreeBeats()
    {
        yield return new WaitForSeconds ((float)audioparser.beat3int);
    }

    void MType3()
    {
        StartCoroutine(WaitThreeBeats());
        RM.TriggerNextKnife();
    }

    

}
