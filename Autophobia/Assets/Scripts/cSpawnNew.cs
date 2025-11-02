using UnityEngine;
using System.Collections;

public class cSpawnNew : MonoBehaviour
{
    const int bpm = 80;
    const int beatsPerMeasure = 4;
    const float offset = 0.08f;  // Offset in seconds (adjust as needed)
    public GameObject circlePrefab;
    public Transform[] spawnPoints;
    public AudioSource audio;
    
    private GameObject currentCircle;
    private int currentMeasure = -1;

    private int index;
    
    void Start()
    {
        currentMeasure = -1;
        index = -1;
    }
    
    public void SpawnCircle()
    {
        index = (index + 1) % 6;
        Transform chosenPoint = spawnPoints[index];
        currentCircle = Instantiate(circlePrefab, chosenPoint);
        currentCircle.transform.localPosition = Vector3.zero;
    }
    
    

    void Update()
    {
        // Calculate seconds per beat
        float secondsPerBeat = 60f / bpm;
        // Calculate seconds per measure
        float secondsPerMeasure = secondsPerBeat * beatsPerMeasure;

        float adjustedTime = audio.time - offset;
        
        // Calculate which measure we're currently in
        int measureNumber = Mathf.FloorToInt(adjustedTime / secondsPerMeasure);

        
        // If we've entered a new measure, spawn a circle
        if (measureNumber > currentMeasure)
        {
            // SpawnOnMeasure();
            currentMeasure = measureNumber;
            SpawnCircle();
        }
    }
}