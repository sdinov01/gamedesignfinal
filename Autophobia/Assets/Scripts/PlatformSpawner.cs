using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public      GameObject      self;
    public      GameObject      ballspawn;
    private     GameObject      ball;
    private     int             index;
    private     int             previdx;
    [SerializeField] public GameObject[] spawnpoints;
    [SerializeField] public GameObject[] destinations;
    private     int         childcount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childcount  =   self.transform.childCount;
        index       =   0;
        previdx     =   0;
    }

    void OnEnable()
    {
        BeatSync.OnMeasure += OnMeasure;
    }

    void OnMeasure()
    {
        previdx = index;
        while (index == previdx)
        {
            index = Random.Range (0, 4);
        }
        // ball = Instantiate (ballspawn, spawnpoints[index].transform.position, Quaternion.identity, spawnpoints[index]);
        ball = Instantiate (ballspawn, spawnpoints[index].transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
