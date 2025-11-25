using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalEnd : MonoBehaviour
{
    public float trackLengthSeconds;
    public Timer timer;

    public int pointsneeded;
    public ScoreManager SM; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.time >= trackLengthSeconds)
        {
            if (SM.successes >= pointsneeded )
            {
                SceneManager.LoadScene("Lust_end_dialogue");
            }
            else
            {
                SceneManager.LoadScene("GameOver_Scene");
            }
        }
    }
}
