using UnityEngine;
using System.Collections;

public class spiderSpawner : MonoBehaviour
{
    public float spawnCooldown = 1f;

    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject gluttonyIntroHandler;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private Transform spawn3;
    [SerializeField] private Transform spawn4;

    [SerializeField] private Transform dest1;
    [SerializeField] private Transform dest2;
    [SerializeField] private Transform dest3;
    [SerializeField] private Transform dest4;

    [SerializeField] private AudioSource audioSource;
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

    /* By default it starts at 15. Determines when to start spawning and pulsing */
    private float startTime = 14.5f;
    public float offset;
    private bool skippedAlready = false;

    [SerializeField] private TimeBar timeBar;
   

    private void Start()
    {

        StartCoroutine(SpawnWhileAudioPlaying());
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.time >= startTime);

        /* Begin song Courotine fill bar */
        timeBar.SetDuration(audioSource.clip.length);
        timeBar.BeginTime();

        while (audioSource.isPlaying)
        {
            /* Don't spawn if the spiders are currently vulnerable */
            yield return new WaitUntil(() => spiderPrefab.GetComponent<spiderMovement>().CanMove());

            SpawnSpider(spawn1, dest1);
            SpawnSpider(spawn2, dest2);
            SpawnSpider(spawn3, dest3);
            SpawnSpider(spawn4, dest4);

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void SpawnSpider(Transform origin, Transform destination)
    {
        // new spider
        GameObject newSpider = Instantiate(spiderPrefab, origin.position, Quaternion.identity);

        // retrieve script
        spiderMovement move = newSpider.GetComponent<spiderMovement>();

        // initialize origin and destination
        move.SetOriginAndDestination(origin, destination);

        // allow it to move
        move.UpdateMove(true);
    }

    void Update()
    {
        
        /* If the tutorial is skipped, make it the new start time */
        if (gluttonyIntroHandler.GetComponent<gluttonyIntro>().SkippedTutorial() && !skippedAlready)
        {
            startTime = Time.time;
            skippedAlready = true;
        }
        /* If there are no more time stamps, then spiders can move */
        if (currentTime > pulseTimeStamps.Length - 1)
        {
            Debug.Log("No more time stamps");
            return;
        }

        /* Convert time stamp in song to second to pulse */
        float pulseTime = convertToSecond(pulseTimeStamps[currentTime]);

        /* Time to do pulse */
        if (Time.time >= pulseTime)
        {
            /* Make it so spiders can't move and pulse occurs */
            spiderPrefab.GetComponent<spiderMovement>().SetDuration(pulseDuration[currentTime]);
            spiderPrefab.GetComponent<spiderMovement>().UpdateMove(false);
            /* If spiders can move, then update currentTime */
            currentTime++;
        }
       
        
    }

    private float convertToSecond(float timeStamp)
    {
        /* First add startTime */
        float second = startTime;
        /* Then add the second offset to the time stamp in the music */
        second += (timeStamp + offset);
        return second;
    }

   

}