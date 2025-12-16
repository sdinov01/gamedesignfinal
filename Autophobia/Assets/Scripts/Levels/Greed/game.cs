/* Handle rotations corresponding to music */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    /* Health bar to keep track of whether the player loses */
    [SerializeField] private healthBar health;
    /* greedIntro to get starting time */
    [SerializeField] private greedIntro intro;
    [SerializeField] private handMovement hourMovement;
    private float startTime = 12.5f;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Image fillAmount;
    private Coroutine performRotationCoroutine;

    void Start()
    {
        performRotationCoroutine = StartCoroutine(hourMovement.performRotations());
    }

    // Update is called once per frame
    void Update()
    {
        /* Update start time */
        float newTime = intro.StartTime();
        if (newTime != startTime)
        {
            startTime = newTime;
            StopCoroutine(performRotationCoroutine);
            StartCoroutine(hourMovement.performRotations());
        }
        if (fillAmount.fillAmount == 1f && Time.timeSinceLevelLoad > startTime)
        {
            Debug.Log("Greed Complete");
            levelTracker.greedComplete = true;
        }

    }

}
