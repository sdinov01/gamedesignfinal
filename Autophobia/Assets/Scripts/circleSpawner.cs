using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
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
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        //copy the circle prefab
        Instantiate(circlePrefab, randomPos, Quaternion.identity);
    }
}
