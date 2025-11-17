/* Handle rotations corresponding to music */
using UnityEngine;
using System.Collections;

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
    /* Range for each area that the player cannot be in */

    /* The minute hand to rotate */
    [SerializeField] private GameObject minuteHand;
    private handMovement minuteHandMovement;
    /* Next rotation to perform */
    private int currRotation = 0;
    /* Time of rotations is the same */
    public float rotationTime = 2f;

    void Start()
    {
        minuteHandMovement = minuteHand.GetComponent<handMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Keep track of time */
        time += Time.deltaTime;
        if (currRotation < timeStamps.Length && currRotation < rotations.Length)
        {
            /* Time to perform a rotation */
            if (time >= timeStamps[currRotation])
            {

                /* If there is another rotation after this one: */
                if (currRotation + 1 < timeStamps.Length)
                {
                    /* and the rotation is soon, rotation time will be the difference */
                    if (timeStamps[currRotation + 1] - timeStamps[currRotation] < 1f)
                    {
                        rotationTime = timeStamps[currRotation + 1] - timeStamps[currRotation];
                    }
                    //else
                    //{
                    //    /* If the next rotation isn't soon, do the same speed as previous rotation */
                    //    rotationTime = 0.5f;
                    //}
                } else
                {
                    /* If there is no next rotation, it is 2 seconds */
                    rotationTime = 2f;
                }
                    minuteHandMovement.PerformRotation(rotations[currRotation], rotationTime);
                /* Starts changing color of clock areas */
                StartCoroutine(ChangeColor(area[currRotation]));
                /* Move on to next rotation */
                currRotation = currRotation + 1;
            }
        }  
    }

    /* Changes the color of an area of the clock */
    private IEnumerator ChangeColor(GameObject area)
    {
        Renderer colorRenderer = area.GetComponent<Renderer>();
        Debug.Log("ROTATION TIME: " + rotationTime);
        yield return new WaitForSeconds(rotationTime);
        colorRenderer.material.SetColor("_Color", colors[0]); // warning color
        yield return new WaitForSeconds(3f);
        colorRenderer.material.SetColor("_Color", colors[1]); // about to turn
        yield return new WaitForSeconds(3f);
        colorRenderer.material.SetColor("_Color", colors[2]); // avoid
        yield return new WaitForSeconds(5f);
        colorRenderer.material.SetColor("_Color", colors[3]); // reset
    }
}
