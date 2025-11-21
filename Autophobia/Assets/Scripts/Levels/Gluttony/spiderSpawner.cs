using UnityEngine;
using System.Collections;

public class spiderSpawner : MonoBehaviour
{
    public float spawnCooldown = 1f;

    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private Transform spawn3;
    [SerializeField] private Transform spawn4;

    [SerializeField] private Transform dest1;
    [SerializeField] private Transform dest2;
    [SerializeField] private Transform dest3;
    [SerializeField] private Transform dest4;

    [SerializeField] private AudioSource audioSource;
    private float timeElapsed;
    /* When the spider pauses movement */
    [SerializeField] private float[] timeStamps;
    private int currentTime = 0;

    private void Start()
    {
        timeElapsed = 0f;
        StartCoroutine(SpawnWhileAudioPlaying());
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        // Wait for audio start (or use WaitUntil(audioSource.isPlaying))
        yield return new WaitForSeconds(15f);

        while (audioSource.isPlaying)
        {
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
        /* If there are no more time stamps, then spiders can move */
        if (currentTime > timeStamps.Length - 1)
        {
            //spiderPrefab.GetComponent<spiderMovement>().UpdateMove(true);
            return;
        }
        timeElapsed += Time.deltaTime;
        /* Time to do pulse */
        if (timeElapsed >= timeStamps[currentTime])
        {
            /* Make it so spiders can't move and pulse occurs */
            spiderPrefab.GetComponent<spiderMovement>().UpdateMove(false);
            /* If spiders can move, then update currentTime */
            currentTime++;
        }
       
        
    }

}