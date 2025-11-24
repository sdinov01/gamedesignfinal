using UnityEngine;
using System.Collections;

public class spiderSpawner : MonoBehaviour
{
    /* Spider prefab */
    [SerializeField] private GameObject spiderPrefab;

    /* Spider spawning and destination locations */
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private Transform spawn3;
    [SerializeField] private Transform spawn4;

    [SerializeField] private Transform dest1;
    [SerializeField] private Transform dest2;
    [SerializeField] private Transform dest3;
    [SerializeField] private Transform dest4;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gluttonyIntroHandler;

    /* When the spider pauses movement */
    [SerializeField] private float[] pulseTimeStamps;
    [SerializeField] private float[] pulseDuration;
    [SerializeField] private float[] spider1Spawn;
    [SerializeField] private float[] spider2Spawn;
    [SerializeField] private float[] spider3Spawn;
    [SerializeField] private float[] spider4Spawn;
    private int spider1index = 0;
    private int spider2index = 0;
    private int spider3index = 0;
    private int spider4index = 0;
    private int currentTime = 0;

    /* Determines when to start spawning and pulsing */
    private float startTime = 14.5f;
    private bool skippedAlready = false;

    [SerializeField] private TimeBar timeBar;
   

    private void Start()
    {

        StartCoroutine(SpawnWhileAudioPlaying());
        skippedAlready = false;
        startTime = 14.5f;
        spider1index = 0;
        spider2index = 0;
        spider3index = 0;
        spider4index = 0;
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= startTime);

        /* Begin song Courotine fill bar */
        timeBar.SetDuration(audioSource.clip.length);
        timeBar.BeginTime();

        while (audioSource.isPlaying)
        {
            /* Don't spawn if the spiders are currently vulnerable */
            if (spider1index < spider1Spawn.Length - 1)
            {
                float time1 = convertToSecond(spider1Spawn[spider1index]);
                if (Time.timeSinceLevelLoad >= time1)
                {
                    SpawnSpider(spawn1, dest1);
                    spider1index++;
                }
            }
            if (spider2index < spider2Spawn.Length - 1)
            {
                float time2 = convertToSecond(spider2Spawn[spider2index]);
                if (Time.timeSinceLevelLoad >= time2)
                {
                    SpawnSpider(spawn2, dest2);
                    spider2index++;
                }
            }
            if (spider3index < spider3Spawn.Length - 1)
            {
                float time3 = convertToSecond(spider3Spawn[spider3index]);
                if (Time.timeSinceLevelLoad >= time3)
                {
                    SpawnSpider(spawn3, dest3);
                    spider3index++;
                }
            }
            if (spider4index < spider4Spawn.Length - 1)
            {
                float time4 = convertToSecond(spider4Spawn[spider4index]);
                if (Time.timeSinceLevelLoad >= time4)
                {
                    SpawnSpider(spawn4, dest4);
                    spider4index++;
                }
            }          
            yield return null;

        }
    }

    private void SpawnSpider(Transform origin, Transform destination)
    {
        /* Spawn a new spider and initialize its origin and destination */
        GameObject newSpider = Instantiate(spiderPrefab, origin.position, Quaternion.identity);
        spiderMovement move = newSpider.GetComponent<spiderMovement>();
        move.SetOriginAndDestination(origin, destination);
    }

    void Update()
    {
        
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
        float pulseTime = convertToSecond(pulseTimeStamps[currentTime]);

        /* Time to do pulse */
        if (Time.timeSinceLevelLoad >= pulseTime)
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