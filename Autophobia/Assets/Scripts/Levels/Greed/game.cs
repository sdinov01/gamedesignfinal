/* Handle rotations corresponding to music */
using UnityEngine;

public class game : MonoBehaviour
{
    /* Contains n many timeStamps that have a start rotation time and end rotation time. */
    private float time = 0;
    [SerializeField] private float[] timeStamps;
    [SerializeField] private float[] rotations;
    [SerializeField] private GameObject minuteHand;
    private handMovement minuteHandMovement;
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
        /* Perform rotations */
        if (currRotation < timeStamps.Length && currRotation < rotations.Length)
        {
            if (time >= timeStamps[currRotation])
            {
                minuteHandMovement.PerformRotation(rotations[currRotation], rotationTime);
                /* Move on to next rotation */
                currRotation = currRotation + 1;
            }
        }  
    }
}
