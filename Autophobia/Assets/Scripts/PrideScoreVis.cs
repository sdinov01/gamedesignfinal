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
            switch (PS.measureIndex) {
                case 12: stageClear (0, 4, 5); break;
                case 20: stageClear (1, 5, 6); break;
                case 28: stageClear (2, 6, 7); break;
                case 36: stageClear (3, 7, 10); break;
                case 44: stageClear (4, 10, 12); break;
                case 53: stageClear (5, 12, 0); break;
            }
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
        SM.NewStage();
    }

    void stageClear (int cindex, int tscore, int newtarget)
    {
        SpriteRenderer sr = transform.GetChild(cindex).GetComponent<SpriteRenderer>();
        ColorChange (sr, (SM.sscore >= tscore));
        resetStageTarget (newtarget);
    }

}
