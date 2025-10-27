using UnityEngine;
using System.Collections;

public class cSpawnNew : MonoBehaviour
{
    const int tempomarker = 45;
    const int   permeasure = 4;

    public GameObject circlePrefab;      // circle prefab
    public Transform[] spawnPoints;     

    private GameObject currentCircle;  

    private int beat;
    private bool onbeat;

    private int measure;

    private int index;

    void Start()
    {
        // SpawnCircle();
        beat = 0;
        measure = 0;
        index = 0;
    }

    public void SpawnCircle()
    {
        index = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[index];

            // generate circle at chosen position and set certain pertal as parent
        currentCircle = Instantiate(circlePrefab, chosenPoint);
        currentCircle.transform.localPosition = Vector3.zero; 

    }

    void Update()
    {
        int beatframe = (int)(tempomarker * 16);
        onbeat = (Time.frameCount % beatframe == 0);
        measure = Time.frameCount / (beatframe * 4);

        



        if (onbeat == true)
        {

            if (measure < 2) {
                
            } else if (measure < 16) {
                if ((beat == 0))
                {
                    SpawnCircle();
                }
            }
            
            // else if (measure < 18) {
            //     if (((beat == 1) || (beat == 3))) 
            //     {
            //         SpawnCircle();
            //         // int index = Random.Range(0, spawnPoints.Length);
            //         // Transform chosenPoint = spawnPoints[index];

            //         //     // generate circle at chosen position and set certain pertal as parent
            //         // currentCircle = Instantiate(circlePrefab, chosenPoint);
            //         // currentCircle.transform.localPosition = Vector3.zero; 
            //     }
            // }
            beat = (beat + 1) % (permeasure * 2);


            // int index = Random.Range(0, spawnPoints.Length);
            // Transform chosenPoint = spawnPoints[index];

            // // generate circle at chosen position and set certain pertal as parent
            // currentCircle = Instantiate(circlePrefab, chosenPoint);
            // currentCircle.transform.localPosition = Vector3.zero; 
            
        }
        
    }

    


}