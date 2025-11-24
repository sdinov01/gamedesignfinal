using UnityEngine;
using System.Collections;

public class handMovement : MonoBehaviour
{
    private bool isRotating = false;
    private float remainingRotation;   // how many degrees left
    private float speed;               // degrees per second (can be ±)
    private GameObject currentSlice = null;
    [SerializeField] private game canRotate;
    [SerializeField] private AudioSource audio;
    [SerializeField] private float[] rotationAmount; // how much to rotate: ex. 30 degrees, 60, 30
    [SerializeField] private float[] changeRotation; // when to change rotation: ex. 6th index, 8th index of rotatointimes.
    private int rotationIndex = 0;
    [SerializeField] private float[] rotationDuration; // how long the rotation will last. ex. 2 seconds, 5 seconds
    [SerializeField] private float[] rotationTimes; // when to rotate.
    private int currentRotation = 0;
    // Ex. 30 degree rotations will occur until 5 second past, then 60 degree rotations of 5 seocnds. 

  

    public IEnumerator performRotations()
    {
        float rotationAmt = rotationAmount[rotationIndex];
        float currentDuration = rotationDuration[rotationIndex];
        /* Begin rotations when audio starts playing */
        yield return new WaitUntil(() => audio.isPlaying);
        /* The rotations will stop if changeRotation is -1. */
        while (audio.isPlaying)
        {
            /* Time to perform a rotation */
            yield return new WaitUntil(() => audio.time >= rotationTimes[currentRotation]);
            Debug.Log("I am rotating");
            /* Rotation and its duration will change */
            if (rotationIndex < changeRotation.Length && currentRotation < rotationTimes.Length)
            {
                if (currentRotation >= changeRotation[rotationIndex])
                {
                    rotationIndex++;
                    rotationAmt = rotationAmount[rotationIndex];
                    currentDuration = rotationDuration[rotationIndex];
                }
            }

            /* Perform rotation */
            yield return StartCoroutine(PerformRotation(rotationAmt, currentDuration));
            currentRotation++;
            //StartCoroutine(ChangeColor(rotationTime));
        }
    }

    public IEnumerator PerformRotation(float rotation, float time)
    {
        float elapsed = 0f;
        float startZ = transform.eulerAngles.z;
        float targetZ = startZ + rotation;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            float newZ = Mathf.Lerp(startZ, targetZ, t);
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                newZ
            );
            yield return null;
        }

        // Snap exactly to final angle
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            targetZ
        );
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        currentSlice = collision.gameObject;
    }

    public GameObject GetCurrentSlice()
    {
        return currentSlice;
    }
}
