using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;      // circle prefab
    public Transform[] spawnPoints;     

    private GameObject currentCircle;  

    void Start()
    {
        SpawnCircle();
    }

    public void SpawnCircle()
    {
        if (currentCircle != null)
        {
            Destroy(currentCircle);
        }

        // randomly choose one petal
        int index = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[index];

        // generate circle at chosen position and set certain pertal as parent
        currentCircle = Instantiate(circlePrefab, chosenPoint);
        currentCircle.transform.localPosition = Vector3.zero; 
    }
}