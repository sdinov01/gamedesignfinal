using UnityEngine;
using System;
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
            //  Stage 2 : 9 balls, 7 points to win
            } else if ((measureIndex < 24) && (measureIndex > 11) && ((measureIndex % 4) < 3)) {
                SpawnBall();
            } 
            

        }
        if (measureIndex == 11) { readyStageEnd = true; }
        if ((measureIndex == 12) && readyStageEnd )
        {
            StageEnd?.Invoke();
            readyStageEnd = false;
        }
        if (measureIndex == 23) { readyStageEnd = true; }
        if ((measureIndex == 24) && readyStageEnd )
        {
            StageEnd?.Invoke();
            readyStageEnd = false;
        }


    }
    void MType1()
    {
        SpawnBall();
    }

}
