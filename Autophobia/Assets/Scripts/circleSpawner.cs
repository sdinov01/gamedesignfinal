using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public Transform[] spawnPoints;
    public AudioSource musicSource;
    public float bpm;
    public float beatsPerMeasure;

    private float secondsPerBeat;
    public List<float> spawnTimes = new List<float>();
    private int nextIndex = 0;

    private List<circle> circles = new List<circle>();
    public Animator animator;
    public TMP_Text beatHintText;
    private int currentCircleIndex = 0;//for test, can be changed to random
    void Start()
    {
        secondsPerBeat = 60f / bpm;
        float secondsPerMeasure = secondsPerBeat * beatsPerMeasure;

        // generate all eyes
        foreach (Transform point in spawnPoints)
        {
            GameObject newCircle = Instantiate(circlePrefab, point);
            newCircle.transform.localPosition = Vector3.zero;
            newCircle.transform.localRotation = Quaternion.identity;

            circle c = newCircle.GetComponent<circle>();
            c.musicSource = musicSource;
            circles.Add(c);

            Animator anim = newCircle.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("Idle", 0, 0f);
                anim.ResetTrigger("hit");
                anim.ResetTrigger("miss");
                anim.SetBool("beat", false);
            }
        }

        for (int i = 1; i <= 50; i++)
        {
            float st = i * secondsPerMeasure;
            spawnTimes.Add(st);
        }
    }

    void Update()
    {
        if (nextIndex < spawnTimes.Count && musicSource.time >= spawnTimes[nextIndex])
        {
            TriggerBeat();
            Debug.Log("beat!");
            nextIndex++;
        }
        /* Always update score */
        if (totalPossibleScore != 0)
        {
            score = currScore / totalPossibleScore;
        }
        FindObjectOfType<GameHandler>().UpdateScore(score);
    }

    void TriggerBeat()
    {
        //int index = Random.Range(0, circles.Count);
        circle chosen = circles[currentCircleIndex];
        Debug.Log("beat2");
        /* Update total score */
        totalPossibleScore += perfect;
        chosen.TriggerBeat();
        // tell gamehandler which one should be beaten
        FindObjectOfType<GameHandler>().SetCurrentCircle(chosen);
        currentCircleIndex = (currentCircleIndex + 1) % 6;

    }

}
// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class CircleSpawner : MonoBehaviour {
//     public GameObject circlePrefab; 

//     public Camera camera;

//     public float bpm;
//     public float beatsPerMeasure;

//     public int doubletime;
//     public int everybeat;
//     const float offset = 0.08f;
//     private float secondsPerBeat;
//     public Transform[] spawnPoints;
//     private Color[] colors = {Color.red, Color.green, Color.blue};
//     private GameObject currentCircle;  
//     public AudioSource musicSource;
//     public List<float> spawnTimes = new List<float>();
//     private int nextIndex = 0;
//     private int currColorIndex;

//     void Start() {
//         currColorIndex = -1;
//         secondsPerBeat = 60f / bpm;
//         float secondsPerMeasure = secondsPerBeat * beatsPerMeasure;
//         for (int i = 1; i < 50; i++)
//         {
//             float st = (i * secondsPerMeasure);
//             spawnTimes.Add(st);
//             if ((i > doubletime) && (i <= everybeat))
//             {
//                 float sh = st + (secondsPerMeasure / 2);
//                 spawnTimes.Add(sh);
//             }
//             else if ((i > everybeat))
//             {
//                 for (int j = 1; j < beatsPerMeasure; j++)
//                 {
//                     st = st + secondsPerBeat;
//                     spawnTimes.Add(st);
//                 }
//             }

//         }
//     }

//     void Update() {
//         if(nextIndex < spawnTimes.Count && musicSource.time >= spawnTimes[nextIndex]) {
//             SpawnCircle(spawnTimes[nextIndex]);
//             nextIndex++;
//         }
//     }

//     public void SpawnCircle(float targetTime) {
//         // randomly choose one petal
//         int index = Random.Range(0, spawnPoints.Length);
//         Transform chosenPoint = spawnPoints[index];

//         // generate circle at chosen position and set certain pertal as parent
//         currentCircle = Instantiate(circlePrefab, chosenPoint);
//         currentCircle.transform.localPosition = Vector3.zero;
        
//         currentCircle.transform.localRotation = Quaternion.identity;
//         circle c = currentCircle.GetComponent<circle>();
//         c.spawnTime = targetTime;
//         c.musicSource = musicSource;
//     }

//     public void ColorChange() {
//         Color first = camera.backgroundColor;
//         int index = 0;
//         while (index == currColorIndex)
//         {
//             index = Random.Range(0, colors.Length);
//         }
//         currColorIndex = index;

//     }
// }