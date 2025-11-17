/* Handle rotations corresponding to music */
using UnityEngine;

public class game : MonoBehaviour
{
    /* Contains n many timeStamps that have a start rotation time and end rotation time. */
    private float time = 0;
    /* Time to start rotation */
    [SerializeField] private float[] timeStamps;
    /* The rotation */
    [SerializeField] private float[] rotations;
    /* The area of the clock to change color */
    [SerializeField] private int[] area;

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
            /* A rotation must be performed if this is true */
            if (time >= timeStamps[currRotation])
            {
                /* Performs rotation */
                minuteHandMovement.PerformRotation(rotations[currRotation], rotationTime);
                /* Starts changing color of clock areas */
                ChangeColor(area[currRotation]);
                /* Move on to next rotation */
                currRotation = currRotation + 1;
            }
        }  
    }

    /* Changes the color of an area of the clock */
    void ChangeColor(int area)
    {

    }
}
