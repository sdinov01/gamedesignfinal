using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public Transform spawnPoint;
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -3f;
    public float maxY = 3f;

    void Start()
    {
        SpawnCircle();
    }

    public void SpawnCircle()
    {
        if (spawnPoint != null) {
            Instantiate(circlePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
