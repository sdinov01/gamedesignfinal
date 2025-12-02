using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class CircleSpawner_new : MonoBehaviour
{
    public AudioSource musicSource;
    public Circle[] eyes;  
    public Light2D[] lights;
    public List<float> spawnTimes = new List<float>();    
    public float bpm = 90;

    public List<int> spawnPlace = new List<int>();
    private int index = 0; //for eye prefabs
    private int nextIndex = 0; //for spawn time

    private float idleIntensity = 0.16f;
    private float activeIntensity = 0.8f;
    private float lightFadeTime = 1.09f;

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
            StartCoroutine(AnimateLight(lights[spawnPlace[index]]));
            index = (index + 1);
            // % eyes.Length;
        }
    }
    IEnumerator AnimateLight(Light2D light)
    {
        //intensity 0.3 → 0.7
        float t = 0f;
        while (t < lightFadeTime)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(idleIntensity, activeIntensity, t / lightFadeTime);
            yield return null;
        }

        yield return new WaitForSeconds(1.09f); // wait for the eye close animation done

        // set back to 0.3
        t = 0f;
        while (t < lightFadeTime)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(activeIntensity, idleIntensity, t / lightFadeTime);
            yield return null;
        }

        light.intensity = idleIntensity;
    }
}
    