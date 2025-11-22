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
    /* The area of the clock to change color */
    [SerializeField] private GameObject[] area;
    /* Colors for changing the clock slices */
    [SerializeField] private Color[] colors;
    /* Health bar to keep track of whether the player loses */
    [SerializeField] private healthBar health;
    /* greedIntro to get starting time */
    [SerializeField] private greedIntro intro;

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
    public float offset = 2f;

    void Start()
    {
        minuteHandMovement = minuteHand.GetComponent<handMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Update start time */
        startTime = intro.StartTime();


        /* Keep track of time */
        if (currRotation < timeStamps.Length && currRotation < rotations.Length)
        {
            float rotation = calculateToSecond(timeStamps[currRotation]);
            Debug.Log("Rotation is at second: " + rotation);
            /* Time to perform a rotation */
            if (Time.time >= rotation)
            {

                /* If there is another rotation after this one: */
                if (currRotation + 1 < timeStamps.Length)
                {
                    /* and the rotation is soon, rotation time will be the difference */
                    float nextRotation = calculateToSecond(timeStamps[currRotation + 1]);
                    Debug.Log("Next rotation is at second: " + nextRotation);
                    if (nextRotation - rotation < 1f)
                    {
                        rotationTime = nextRotation - rotation;
                    }
                } else
                {
                    /* If there is no next rotation, the rotation will last 2 seconds */
                    rotationTime = 2f;
                }
                minuteHandMovement.PerformRotation(rotations[currRotation], rotationTime);
                /* Starts changing color of clock areas */
                StartCoroutine(ChangeColor(area[currRotation]));
                /* Move on to next rotation */
                currRotation = currRotation + 1;
            }
        } else
        {
            /* We finished rotations */
            StartCoroutine(endGame(15f));

        } 
    }

    /* Changes the color of an area of the clock */
    private IEnumerator ChangeColor(GameObject area)
    {
        // get color component
        Renderer colorRenderer = area.GetComponent<Renderer>();
        yield return new WaitForSeconds(rotationTime); // wait until the hour hand reaches the slice before turning color
        colorRenderer.material.SetColor("_Color", colors[0]); // warning color
        yield return new WaitForSeconds(3f);
        colorRenderer.material.SetColor("_Color", colors[1]); // about to turn
        yield return new WaitForSeconds(3f);
        colorRenderer.material.SetColor("_Color", colors[2]); // avoid
        yield return new WaitForSeconds(5f);
        colorRenderer.material.SetColor("_Color", colors[3]); // reset
    }

    /* When the level is complete, go to Level_Select scene */
    private IEnumerator endGame(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level_Select_Scene");
    }

    private float calculateToSecond(float timeStamp)
    {
        /* First add startTime */
        float second = startTime;
        /* Then add the second offset to the time stamp in the music */
        second += (timeStamp + offset);
        return second;
    }
}
