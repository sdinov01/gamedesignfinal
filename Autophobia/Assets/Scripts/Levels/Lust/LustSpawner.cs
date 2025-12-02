using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LustSpawner : MonoBehaviour
{
    /* Enemy prefab */
    [SerializeField] private GameObject lip1;
    [SerializeField] private GameObject lip2;

    /* Spawning origin */
    [SerializeField] private Transform[] spawns;

    /* Destination */
    [SerializeField] private Transform[] destinations;

    /* Indices to spawn enemies */
    private int[] spawnIndices;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gluttonyIntroHandler;

    /* Lip Spawning */
    [SerializeField] private float[] lipSpawn1;
    [SerializeField] private float[] lipSpawn2;
    [SerializeField] private float[] lipSpawn3;
    [SerializeField] private float[] lipSpawn4;
    [SerializeField] private float[] otherLipSpawn1;
    [SerializeField] private float[] otherLipSpawn2;
    [SerializeField] private float[] otherLipSpawn3;
    [SerializeField] private float[] otherLipSpawn4;
    private int currentTime = 0;

    /* Lip spawning locations */
    private float[][] spawnTimes;


    /* Determines when to start spawning and pulsing */
    private float startTime = 14.5f;
    private bool skippedAlready = false;

    [SerializeField] private TimeBar timeBar;
    public healthBar health;

    public float orientation;
    public float orientation2;


    private void Start()
    {

        StartCoroutine(SpawnWhileAudioPlaying());
        skippedAlready = false;
        startTime = 14.5f;
        spawnIndices = new int[spawns.Length];
        spawnTimes = new float[spawns.Length][];
        /* Add the spawns manually */
        spawnTimes[0] = lipSpawn1;
        spawnTimes[1] = lipSpawn2;
        spawnTimes[2] = lipSpawn3;
        spawnTimes[3] = lipSpawn4;
        spawnTimes[4] = otherLipSpawn1;
        spawnTimes[5] = otherLipSpawn2;
        spawnTimes[6] = otherLipSpawn3;
        spawnTimes[7] = otherLipSpawn4;
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= startTime);

        /* Begin song Courotine fill bar */
        timeBar.SetDuration(audioSource.clip.length);
        timeBar.BeginTime();
        float finishSong = startTime + audioSource.clip.length;

        while (audioSource.time < audioSource.clip.length)
        {
            /* Go through each spawner and check if it's time to spawn a spider */
            for (int spawner = 0; spawner < spawns.Length; spawner++)
            {
                /* Retrieve the index for the spawn time */
                int toSpawn = spawnIndices[spawner];
                /* This index must be a valid index */
                if (toSpawn < spawnTimes[spawner].Length - 1)
                {
                    /* Retrieve the spawn time using the index */
                    float time = convertToSecond(spawnTimes[spawner][toSpawn]);
                    /* Check if it is time to spawn that spider */
                    if (Time.timeSinceLevelLoad >= time)
                    {
                        /* If it is time, spawn the spider and assign its origin and destination */
                        SpawnLip(spawns[spawner], destinations[spawner], (spawner >= 4));
                        /* Update the index */
                        spawnIndices[spawner]++;
                    }
                }
            }
            yield return null;

        }
    }

    private void SpawnLip(Transform origin, Transform destination, bool heal)
    {
        /* Spawn a new spider and initialize its origin and destination */
        GameObject newSpider;
        if (heal && lip2 != null)
        {
            newSpider = Instantiate(lip2, origin.position, Quaternion.identity);
        }
        else if (lip1 != null)
        {
            newSpider = Instantiate(lip1, origin.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("One of the prefabs is not set");
            return;
        }

        spiderMovement move = newSpider.GetComponent<spiderMovement>();
        move.SetOriginAndDestination(origin, destination);
        if (heal)
        {
            move.SetStartingOrientation(orientation2);
        } else
        {
            move.SetStartingOrientation(orientation);
        }
    }

    void Update()
    {
        checkMusicEnd();

        /* If the tutorial is skipped, make it the new start time */
        if (gluttonyIntroHandler.GetComponent<gluttonyIntro>().SkippedTutorial() && !skippedAlready)
        {
            startTime = Time.timeSinceLevelLoad;
            skippedAlready = true;
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

    void checkMusicEnd()
    {

        if (!audioSource.isPlaying)
        {
            if (health.healthLeft() > 0 && audioSource.time >= audioSource.clip.length - 0.1f)
            {
                Debug.Log("end");
                string sceneName = SceneManager.GetActiveScene().name;
                if (sceneName == "Gluttony_Level")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("gluttony_end_dialogue");
                }
                else if (sceneName == "Lust_Level")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("lust_end_dialogue");
                }
                else if (sceneName == "Envy_Level")
                {
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("gluttony_end_dialogue");
                    // does not exist yet please add.
                }
            }
        }
    }


}