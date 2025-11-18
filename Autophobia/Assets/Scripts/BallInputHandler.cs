using UnityEngine;

public class BallInputHandler : MonoBehaviour
{
    private bool isAtMaxSize = false;
    [SerializeField] private float inputWindow = 0.15f; // seconds before/after beat
    [SerializeField] private string playerTag = "Player"; // Tag for the player object
    
    private double lastBeatTime;
    private bool canScore = true;
    private bool playerTouching = false;

    void OnEnable()
    {
        BeatSync.OnBeat += OnBeat;
    }

    void OnDisable()
    {
        BeatSync.OnBeat -= OnBeat;
    }

    void OnBeat()
    {
        lastBeatTime = AudioSettings.dspTime;
        canScore = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canScore)
        {
            double currentTime = AudioSettings.dspTime;
            double timeSinceBeat = currentTime - lastBeatTime;
            
            // Check if within timing window, ball is at max size, AND player is touching
            if (timeSinceBeat <= inputWindow && isAtMaxSize && playerTouching)
            {
                ScorePoint();
                canScore = false; // Prevent multiple scores per beat
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerTouching = false;
        }
    }

    public void SetAtMaxSize(bool atMax)
    {
        isAtMaxSize = atMax;
    }

    private void ScorePoint()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoint();
            Debug.Log("Score!");
        }
    }
}