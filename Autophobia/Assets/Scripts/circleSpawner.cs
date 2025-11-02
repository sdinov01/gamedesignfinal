using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleSpawner : MonoBehaviour {
    public GameObject circlePrefab; 
    public Transform[] spawnPoints;
    private GameObject currentCircle;  
    public AudioSource musicSource;
    public List<float> spawnTimes;
    private int nextIndex = 0;

    void Update() {
        if(nextIndex < spawnTimes.Count && musicSource.time >= spawnTimes[nextIndex]) {
            SpawnCircle(spawnTimes[nextIndex]);
            nextIndex++;
        }
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
    }
}