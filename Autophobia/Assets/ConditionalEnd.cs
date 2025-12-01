using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalEnd : MonoBehaviour
{
    public GameObject SceneFadeOut;
    public float trackLengthSeconds;
    public Timer timer;

    public int pointsneeded;
    public ScoreManager SM; 
    private bool scenecalled = false;
    private GameObject SceneEnding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneEnding = GameObject.FindWithTag ("EndScene");
    }

    // Update is called once per frame
    void Update()
    {
        if ((timer.time >= 92) && !scenecalled)
        {
            scenecalled = true;
            if (SM.successes >= pointsneeded )
            {
                SceneEnding.GetComponent<endscene>().didwin = true;
            }
            SceneEnding.GetComponent<endscene>().ScreenFill();
        }
        // if (timer.time >= trackLengthSeconds)
        // {
        //     if (SM.successes >= pointsneeded )
        //     {
        //         SceneManager.LoadScene("Lust_end_dialogue");
        //     }
        //     else
        //     {
        //         SceneManager.LoadScene("GameOver_Scene");
        //     }
        // }
    }
}
