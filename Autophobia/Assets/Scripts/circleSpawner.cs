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
    const float offset = 1.6f;
    private float secondsPerBeat;
    public Transform[] spawnPoints;
    private Color[] colors = {Color.red, Color.green, Color.blue};
    private GameObject currentCircle;  
    public AudioSource musicSource;
    private List<float> spawnTimes = new List<float>();
    private int nextIndex = 0;
    private int currColorIndex;


    void Start() {
        currColorIndex = -1;
        secondsPerBeat = 60f / bpm;
        float secondsPerMeasure = secondsPerBeat * beatsPerMeasure;
        for (int i = 1; i < 50; i++)
        {
            float st = (i * secondsPerMeasure);
            spawnTimes.Add(st - offset);
            if ((i > doubletime) && (i <= everybeat))
            {
                float sh = st + (secondsPerMeasure / 2);
                spawnTimes.Add(sh - offset);
            }
            else if ((i > everybeat))
            {
                for (int j = 1; j < beatsPerMeasure; j++)
                {
                    st = st + secondsPerBeat;
                    spawnTimes.Add(st - offset);
                }
            }

        }
    }

    void Update() {
        if((musicSource.time + offset) == spawnTimes[nextIndex]) {
            ColorChange();
        }

        if(nextIndex < spawnTimes.Count && musicSource.time >= spawnTimes[nextIndex]) {
            SpawnCircle(spawnTimes[nextIndex]);
            nextIndex++;
        }
    }

    public void ColorChange() {
        Color first = camera.backgroundColor;
        int index = -1;
        while (index == currColorIndex)
        {
            index = Random.Range(0, colors.Length);
        }
        currColorIndex = index;
        camera.backgroundColor = Color.Lerp(first, colors[currColorIndex], 0.1f);
    }

    public void SpawnCircle(float targetTime) {
        // randomly choose one petal
        int index = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[index];

        // generate circle at chosen position and set certain pertal as parent
        currentCircle = Instantiate(circlePrefab, chosenPoint);
        currentCircle.transform.localPosition = Vector3.zero;
        
        currentCircle.transform.localRotation = Quaternion.identity;
        circle c = currentCircle.GetComponent<circle>();
        c.spawnTime = targetTime;
        c.musicSource = musicSource;

        StartCoroutine(cchange());
    }

    IEnumerator cchange() {
        yield return new WaitForSeconds(offset);
        ColorChange();
    }


}