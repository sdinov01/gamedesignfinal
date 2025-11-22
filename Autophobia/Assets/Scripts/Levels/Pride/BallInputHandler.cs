using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallInputHandler : MonoBehaviour
{
    private bool isAtMaxSize = false;
    [SerializeField] private float inputWindow = 0.15f; // seconds before/after beat
    [SerializeField] private string playerTag = "Player"; // Tag for the player object
    
    private double lastBeatTime;
    private bool canScore = true;
    public GameObject player;
    private bool playerTouching = false;

    private Collider2D thiscollider;
    private Collider2D playercollider;

    void Start()
    {
        player = GameObject.FindWithTag (playerTag);

        thiscollider    = transform.GetComponent<Collider2D>();
        playercollider  = player.GetComponent<Collider2D>();
    }

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
        bool colliderin = thiscollider.IsTouching(playercollider);

        if (Input.GetKeyDown(KeyCode.Space) && canScore)
        {
            double currentTime = AudioSettings.dspTime;
            double timeSinceBeat = currentTime - lastBeatTime;
            
            // Check if within timing window, ball is at max size, AND player is touching
            if (timeSinceBeat <= inputWindow && isAtMaxSize && colliderin)
            {
                ScorePoint();
                canScore = false; // Prevent multiple scores per beat
            }
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