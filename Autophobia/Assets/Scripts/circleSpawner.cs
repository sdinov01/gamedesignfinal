using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleSpawner : MonoBehaviour {
    public GameObject circlePrefab; 

    public Camera camera;

    public float bpm;
    public float beatsPerMeasure;

    public int doubletime;
    public int everybeat;

    const float offset = 0.08f;

    private float secondsPerBeat;

    public Transform[] spawnPoints;
    private GameObject currentCircle;  
    public AudioSource musicSource;
    public List<float> spawnTimes = new List<float>();
    private int nextIndex = 0;

    void Start() {
        secondsPerBeat = 60f / bpm;
        float secondsPerMeasure = secondsPerBeat * beatsPerMeasure;
        for (int i = 1; i < 50; i++)
        {
            float st = (i * secondsPerMeasure);
            spawnTimes.Add(st);
            if ((i > doubletime) && (i <= everybeat))
            {
                float sh = st + (secondsPerMeasure / 2);
                spawnTimes.Add(sh);
            }
            else if ((i > everybeat))
            {
                for (int j = 1; j < beatsPerMeasure; j++)
                {
                    st = st + secondsPerBeat;
                    spawnTimes.Add(st);
                }
            }

        }
    }

    void Update() {
        if(nextIndex < spawnTimes.Count && musicSource.time >= spawnTimes[nextIndex]) {
            SpawnCircle(spawnTimes[nextIndex]);
            nextIndex++;
        }
    }

    public void SpawnCircle(float targetTime) {
        // randomly choose one petal
        Debug.Log("Hi");

        int index = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[index];

        // generate circle at chosen position and set certain pertal as parent
        currentCircle = Instantiate(circlePrefab, chosenPoint);
        currentCircle.transform.localPosition = Vector3.zero;
        
        currentCircle.transform.localRotation = Quaternion.identity;
        circle c = currentCircle.GetComponent<circle>();
        c.spawnTime = targetTime;
        c.musicSource = musicSource;
    }

    public void ColorChange() {
        Color first = camera.backgroundColor;
        
    }
}