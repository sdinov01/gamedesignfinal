using UnityEngine;

public class BeatBallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform destination;

    [SerializeField] public GameObject[] spawnpoints;

    public GameObject SpawnParent;

    bool canSpawn = true;
    bool spawnedThisBeat = false;

    void OnEnable()
    {
        BeatSync.OnMeasure += TrySpawn;
    }

    void OnDisable()
    {
        BeatSync.OnMeasure -= TrySpawn;
    }

    void LateUpdate()
    {
        // resets the spawn lock AFTER the beat frame
        spawnedThisBeat = false;
    }

    void TrySpawn()
    {
        if (!canSpawn) return;
        if (spawnedThisBeat) return;
        spawnedThisBeat = true;
        
        int index = Random.Range(0, 4);
        GameObject tospawn = spawnpoints[index];
        
        GameObject ball = Instantiate(ballPrefab, tospawn.transform.position, Quaternion.identity);
        
        // Use LerpToTarget instead of LaneRoute
        var lerp = ball.GetComponent<LerpToTarget>();
        if (lerp != null)
        {
            lerp.Begin(destination, 2f); // 2 seconds to reach destination
        }
        
        // Disable LaneRoute if both are present
        var route = ball.GetComponent<LaneRoute>();
        if (route != null) route.enabled = false;
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
