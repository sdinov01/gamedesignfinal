using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class spiderSpawner : MonoBehaviour
{
    /* Enemy prefab */
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;

    /* Spawning origin */
    [SerializeField] private Transform[] spawns;

    /* Destination */
    [SerializeField] private Transform[] destinations;

    /* Indices to spawn enemies */
    private int[] spawnIndices;

    private float[][] spawnTimes;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gluttonyIntroHandler;

    /* When the enemy pauses movement */
    [SerializeField] private float[] pulseTimeStamps;
    [SerializeField] private float[] pulseDuration;
    [SerializeField] private float[] spider1Spawn;
    [SerializeField] private float[] spider2Spawn;
    [SerializeField] private float[] spider3Spawn;
    [SerializeField] private float[] spider4Spawn;
    [SerializeField] private float[] healSpiderSpawn1;
    [SerializeField] private float[] healSpiderSpawn2;
    [SerializeField] private float[] healSpiderSpawn3;
    [SerializeField] private float[] healSpiderSpawn4;
    private int currentTime = 0;


    /* Determines when to start spawning and pulsing */
    //private float startTime = 14.5f;
    private float startTime = 23f;
    private bool skippedAlready = false;

    [SerializeField] private TimeBar timeBar;
    public healthBar health;
   

    private void Start()
    {

        StartCoroutine(SpawnWhileAudioPlaying());
        skippedAlready = false;
        //startTime = 14.5f;
        startTime = gluttonyIntroHandler.GetComponent<gluttonyIntro>().StartTime();
        spawnIndices = new int[spawns.Length];
        spawnTimes = new float[spawns.Length][];
        /* Add the spawns manually */
        spawnTimes[0] = spider1Spawn;
        spawnTimes[1] = spider2Spawn;
        spawnTimes[2] = spider3Spawn;
        spawnTimes[3] = spider4Spawn;
        if (SceneManager.GetActiveScene().name == "Envy_Level")
        {
            Debug.Log("adding heal spiders");
            spawnTimes[4] = healSpiderSpawn1;
            spawnTimes[5] = healSpiderSpawn2;
            spawnTimes[6] = healSpiderSpawn3;
            spawnTimes[7] = healSpiderSpawn4;
        }
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= startTime);

        /* Begin song Courotine fill bar */
        Debug.Log("PLAYING");
        timeBar.SetDuration(audioSource.clip.length);
        timeBar.BeginTime();

        while (audioSource.time < audioSource.clip.length)
        {
            /* Go through each spawner and check if it's time to spawn a spider */
            for (int spawner = 0; spawner < spawns.Length; spawner++)
            {
                /* Retrieve the index for the spawn time */
                if (spawnIndices[spawner] != null)
                {
                    int toSpawn = spawnIndices[spawner];
                    /* This index must be a valid index */
                    if (toSpawn < spawnTimes[spawner].Length - 1)
                    {
                        /* Retrieve the spawn time using the index */
                        float time = spawnTimes[spawner][toSpawn];
                        /* Check if it is time to spawn that spider */
                        if (audioSource.time >= time)
                        {
                            /* If it is time, spawn the spider and assign its origin and destination */
                            SpawnSpider(spawns[spawner], destinations[spawner], (spawner >= 4));
                            /* Update the index */
                            spawnIndices[spawner]++;
                        }
                    }
                }
                
            }     
            yield return null;

        }
    }

    private void SpawnSpider(Transform origin, Transform destination, bool heal)
    {
        /* Spawn a new spider and initialize its origin and destination */
        GameObject newSpider;
        if (heal && enemy2 != null)
        {
            newSpider = Instantiate(enemy2, origin.position, Quaternion.identity);
        } else if (enemy != null)
        {
            newSpider = Instantiate(enemy, origin.position, Quaternion.identity);
        } else
        {
            Debug.Log("One of the prefabs is not set");
            return;
        }

        spiderMovement move = newSpider.GetComponent<spiderMovement>();
        move.SetOriginAndDestination(origin, destination);
    }

    void Update()
    {
        startTime = gluttonyIntroHandler.GetComponent<gluttonyIntro>().StartTime();

        /* If the tutorial is skipped, make it the new start time */
        if (gluttonyIntroHandler.GetComponent<gluttonyIntro>().SkippedTutorial() && !skippedAlready)
        {
            startTime = Time.timeSinceLevelLoad;
            skippedAlready = true;
        }
        /* If there are no more time stamps, then spiders can move */
        if (currentTime > pulseTimeStamps.Length - 1)
        {
            return;
        }

        /* Convert time stamp in song to second to pulse */
        float pulseTime = pulseTimeStamps[currentTime];

        /* Time to do pulse */
        if (audioSource.time >= pulseTime)
        {
            spiderMovement.TriggerPulse(pulseDuration[currentTime]);
            currentTime++;
        }
       
        
    }

    private float convertToSecond(float timeStamp)
    {
        /* First add startTime */
        float second = startTime;
        /* Then add the second offset to the time stamp in the music */
        second += timeStamp;
        return second;
    }

   

}