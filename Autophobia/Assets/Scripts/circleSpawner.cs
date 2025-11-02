using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleSpawner : MonoBehaviour {
    public GameObject circlePrefab; 
    public Transform[] spawnPoints;
    private GameObject currentCircle;  
    public AudioSource musicSource;
    public List<float> spawnTimes; //spawn
    public List<float> targetTimes; //when eye fully opens
    //targetTime - spawnTime = the time interval for eye to open
    private int nextIndex = 0;

    void Update() {
    if(nextIndex < spawnTimes.Count) {
        float spawnTime = spawnTimes[nextIndex];
        if(musicSource.time >= spawnTime) {
            float targetTime = targetTimes[nextIndex];
            SpawnCircle(spawnTime, targetTime);
            nextIndex++; 
        }
    }
}

    public void SpawnCircle(float spawnTime, float targetTime) {
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
    }
}