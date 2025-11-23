using UnityEngine;
using System.Collections;

public class PrideScoreVis : MonoBehaviour
{
    public int numchildren;
    public int childindex = 0;
    private int phasescleared = 0;
    public PlatformSpawner PS;
    public ScoreManager SM;
    private GameObject[] Children;

    public Sprite green;
    public Sprite red;


    void Start()
    {
        numchildren = transform.childCount;
    }

    void OnEnable()
    {
        PlatformSpawner.StageEnd += VSH;
    }

    // Update is called once per frame
    void VSH()
    {
        if (childindex < numchildren)
        {
            if ((PS.measureIndex == 12)) 
            { 
                SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
                ColorChange (sr, (SM.sscore > 3));
                resetStageTarget (7);
            };
            if ((PS.measureIndex == 24)) 
            { 
                SpriteRenderer sr = transform.GetChild(1).GetComponent<SpriteRenderer>();
                ColorChange (sr, (SM.sscore > 6));
                resetStageTarget (9);
            };

        }
    }

    void ColorChange (SpriteRenderer sr, bool b)
    {
        if (b)
        {
            MakeGreen (sr);
        }
        else
        {
            MakeRed (sr);
        }
    }

    void MakeGreen (SpriteRenderer sr)
    {
        sr.sprite = green;
        sr.color = Color.white;
    }

    void MakeRed (SpriteRenderer sr)
    {
        sr.color = Color.white;

    }

    void resetStageTarget (int i)
    {
        SM.target = i;
        SM.sscore = 0;
    }

}
