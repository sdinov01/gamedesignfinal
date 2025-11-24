using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public static event     Action StageEnd;
    public      GameObject      self;
    public      ModularAudioHandler     M;
    public      GameObject      ballspawn;
    private     GameObject      ball;
    private     int             index;
    private     int             previdx;
    [SerializeField] public GameObject[] spawnpoints;
    [SerializeField] public GameObject[] destinations;
    private     int         childcount;
    public      int         measureIndex = 0;
    public      double      time = 0f;
    public      double      nextM;
    private     bool        readyStageEnd = false;

    private     int[]       stageEndFlags = {11, 19, 27, 35, 43, 52};
    private     int[]       stageEndIndex = {12, 20, 28, 36, 44, 53};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childcount  =   self.transform.childCount;
        index       =   0;
        previdx     =   0;
        nextM       =   M.measureint;
    }

    void SpawnBall()
    {
        previdx = index;
        while (index == previdx)
        {
            index = UnityEngine.Random.Range (0, 4);
        }
        ball = Instantiate (ballspawn, spawnpoints[index].transform);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > nextM)
        {
            measureIndex++;
            nextM += M.measureint;
            //  Stage 1 : 6 balls, 4 points to win
            if ((measureIndex < 12) && ((measureIndex % 2)) == 1) {
                SpawnBall();
            //  Stage 2 : 6 balls, 5 points to win
            } else if ((measureIndex < 20) && (measureIndex > 11) && ((measureIndex % 4) < 3)) {
                SpawnBall();
            //  Stage 3 : 8 balls, 6 points to win
            } else if ((measureIndex < 28) && (measureIndex > 19)) {
                SpawnBall();
            //  Stage 4 : 10 balls, 7 points to win
            } else if ((measureIndex < 36) && (measureIndex > 27)) {
                switch ((measureIndex % 4)) {
                    case 0: SpawnBall();                break;
                    case 1: StartCoroutine(MType1());   break;
                    case 2: SpawnBall();                break;
                    case 3: SpawnBall();                break;
                }
            //  Stage 5 : 12 balls, 10 points to win
            } else if ((measureIndex < 44) && (measureIndex > 35)) {
                switch ((measureIndex % 4)) {
                    case 0: SpawnBall();                break;
                    case 1: StartCoroutine(MType1());   break;
                    case 2: SpawnBall();                break;
                    case 3: StartCoroutine(MType1());   break;
                }
            }
            //  1 measure rest... Prepare yourself
            //  Stage 6 : Mayhem! 16 balls, 12 intercepts to win
            else if ((measureIndex < 53) && (measureIndex > 44)) {
                StartCoroutine (MType1());
            }
            

        }
        if ((stageEndFlags.Contains(measureIndex))) { readyStageEnd = true; }
        if ((stageEndIndex.Contains(measureIndex))) { InvokeEnd(); }


        // if (measureIndex == 11) { readyStageEnd = true; }
        // if ((measureIndex == 12) && readyStageEnd )
        // {
        //     StageEnd?.Invoke();
        //     readyStageEnd = false;
        // }
        // if (measureIndex == 19) { readyStageEnd = true; }
        // if ((measureIndex == 20) && readyStageEnd )
        // {
        //     StageEnd?.Invoke();
        //     readyStageEnd = false;
        // }
        // if (measureIndex == 27) { readyStageEnd = true; }
        // if ((measureIndex == 28) && readyStageEnd )
        // {
        //     StageEnd?.Invoke();
        //     readyStageEnd = false;
        // }
        // if (measureIndex == 35) { readyStageEnd = true; }
        // if ((measureIndex == 36) && readyStageEnd )
        // {
        //     StageEnd?.Invoke();
        //     readyStageEnd = false;
        // }
        


    }

    IEnumerator MType1()
    {
        SpawnBall();
        yield return new WaitForSeconds ((float)M.beat2int);
        SpawnBall();
        
    }
    // IEnumerator WaitFor (float f)
    // {
    //     yield return new WaitForSeconds (f);
    // }
    void InvokeEnd()
    {
        readyStageEnd = false;
        StageEnd?.Invoke();
    }

}
