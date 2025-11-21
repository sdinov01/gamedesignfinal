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

    private void Start()
    {
        StartCoroutine(SpawnWhileAudioPlaying());
    }

    private IEnumerator SpawnWhileAudioPlaying()
    {
        // Wait for audio start (or use WaitUntil(audioSource.isPlaying))
        yield return new WaitForSeconds(15f);

        while (audioSource.isPlaying)
        {
            SpawnSpider(spawn1, dest1);
            SpawnSpider(spawn2, dest2);
            SpawnSpider(spawn3, dest3);
            SpawnSpider(spawn4, dest4);

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void SpawnSpider(Transform origin, Transform destination)
    {
        // 1. Instantiate a NEW spider
        GameObject newSpider = Instantiate(spiderPrefab, origin.position, Quaternion.identity);

        // 2. Get its movement script
        spiderMovement move = newSpider.GetComponent<spiderMovement>();

        // 3. Initialize origin + destination for THAT spider
        move.SetOriginAndDestination(origin, destination);

        // 4. Tell it to move
        move.UpdateMove(true);
    }
}