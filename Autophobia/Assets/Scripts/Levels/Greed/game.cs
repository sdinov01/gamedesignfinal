/* Handle rotations corresponding to music */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    /* Contains n many timeStamps that have a start rotation time and end rotation time. */
    private float time = 0;
    /* Time to start rotation */
    [SerializeField] private float[] timeStamps;
    /* The rotation */
    [SerializeField] private float[] rotations;
    //[SerializeField] private float[] changeSpeed;


    //private int currentSpeedIndex = 0;
    //private int currentSpeed;
    /* Colors for changing the clock slices */
    [SerializeField] private Color[] colors;
    /* Health bar to keep track of whether the player loses */
    [SerializeField] private healthBar health;
    /* greedIntro to get starting time */
    [SerializeField] private greedIntro intro;

    [SerializeField] private handMovement hourMovement;

    /* The minute hand to rotate */
    [SerializeField] private GameObject minuteHand;
    private handMovement minuteHandMovement;
    /* Next rotation to perform */
    private int currRotation = 0;
    /* Time of rotations is the same */
    private float rotationTime = 0.5f;
    /* Default start time */
    private float startTime = 14.5f;
    /* Offset */
    private float offset = 0;

    [SerializeField] private float[] colorDuration;
    [SerializeField] private float[] changeDuration;
    private int durationIndex;

    private bool canPerformRotation = true;
    private Coroutine performRotationCoroutine;

    [SerializeField] private AudioSource audio;
    private float previousRotationTime;

    void Start()
    {
        minuteHandMovement = minuteHand.GetComponent<handMovement>();
        durationIndex = 0;
        performRotationCoroutine = StartCoroutine(hourMovement.performRotations());
        previousRotationTime = 0;
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

        /* Time to change duration of the colors */
        if (Time.timeSinceLevelLoad > changeDuration[durationIndex] && durationIndex < colorDuration.Length - 1)
        {
            durationIndex++;
        }


    }

    private IEnumerator ChangeColor(float time)
    {
        /* Wait to finish rotation */
        yield return new WaitForSeconds(time);
        /* Retrieve current slice's renderer to change color */
        GameObject currentSlice = hourMovement.GetCurrentSlice();
        /* Retrieve the duration of the color changing */
        float colorTime = colorDuration[durationIndex];
        Renderer colorRenderer = currentSlice.GetComponent<Renderer>();
        colorRenderer.material.SetColor("_Color", colors[0]); 
        yield return new WaitForSeconds(colorTime);
        colorRenderer.material.SetColor("_Color", colors[1]);
        yield return new WaitForSeconds(colorTime);
        colorRenderer.material.SetColor("_Color", colors[2]); 
        yield return new WaitForSeconds(colorTime);
        colorRenderer.material.SetColor("_Color", colors[3]); 
    }

    /* When the level is complete, go to Level_Select scene */
    private IEnumerator endGame(float time)
    {
        yield return new WaitForSeconds(time);
        levelTracker.greedComplete = true;
        Debug.Log(levelTracker.greedComplete);
    }

    private float calculateToSecond(float timeStamp)
    {
        /* First add startTime */
        float second = startTime;
        /* Then add the second offset to the time stamp in the music */
        second += (timeStamp + offset);
        return second;
    }

    public void CanRotate(bool can)
    {
        canPerformRotation = can;
    }
    
}
