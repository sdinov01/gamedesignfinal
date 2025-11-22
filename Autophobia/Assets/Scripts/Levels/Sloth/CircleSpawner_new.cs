using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleSpawner_new : MonoBehaviour
{
    public AudioSource musicSource;
    public Circle[] eyes;  
    public List<float> spawnTimes = new List<float>();    
    public float bpm = 90;

    public List<int> spawnPlace = new List<int>();
    private int index = 0; //for eye prefabs
    private int nextIndex = 0; //for spawn time

    void Start()
    {
        StartCoroutine(StartBeats());
    }
    
    IEnumerator StartBeats()
    {
        while (musicSource.time <= 0.01f)
            yield return null;

        for (int i = 0; i < spawnTimes.Count; i++)
        {
            float targetTime = spawnTimes[i]; 
            float triggerTime = targetTime - 1.09f; // play animation 1.09 earlier

            while (musicSource.time < triggerTime)
            {
                yield return null;
            }
            eyes[spawnPlace[index]].Trigger(triggerTime); 
            index = (index + 1) % eyes.Length;
        }
    }
}
    