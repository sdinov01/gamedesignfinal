using UnityEngine;
using System.Collections;

public class handMovement : MonoBehaviour
{
    /*                  Rotation variables              */
    
    /* Variables to perform the rotation */
    private bool isRotating = false;
    private float remainingRotation;  
    private float speed;
    /* How much to rotate by */
    [SerializeField] private float[] rotationAmount; 
    /* Which rotationTime (index in array) to change the rotation */
    [SerializeField] private int[] changeRotation; 
    /* How long the rotation will last */
    [SerializeField] private float[] rotationDuration;
    /* Keeps track of the rotation amount and duration */
    private int rotationIndex = 0;
    /* When in the song to begin a rotation */
    [SerializeField] private float[] rotationTimes; 
    /* Keeps track of the current rotation time */
    private int currentRotation = 0;

    
    private GameObject currentSlice = null;
    [SerializeField] private AudioSource audio;
    [SerializeField] private restrictMovement rm;


    public IEnumerator performRotations()
    {
        float rotationAmt = rotationAmount[rotationIndex] * 2;
        float currentDuration = rotationDuration[rotationIndex];
        /* Begin rotations when audio starts playing */
        yield return new WaitUntil(() => audio.isPlaying);
        /* The rotations will stop if changeRotation is -1. */
        while (audio.time < audio.clip.length)
        {
            /* Time to perform a rotation */
            if (currentRotation >= rotationTimes.Length - 1)
            {
                break;
            }
            yield return new WaitUntil(() => audio.time >= rotationTimes[currentRotation]);
            /* Rotation and its duration will change */
            if (rotationIndex < changeRotation.Length && currentRotation < rotationTimes.Length)
            {
                if (currentRotation >= changeRotation[rotationIndex] && rotationIndex < rotationAmount.Length - 1)
                {
                    rotationIndex++;
                    rotationAmt = rotationAmount[rotationIndex] * 2;
                    currentDuration = rotationDuration[rotationIndex];
                }
            }

            /* Perform rotation */
            if (currentRotation % 4 == 1){
                Debug.Log("HI IM ODD");
                rotationAmt -= 15;
            } else if (currentRotation % 4 == 2){
                rotationAmt += 15;
                Debug.Log("HI IM EVEN");
            }
            yield return StartCoroutine(PerformRotation(rotationAmt, currentDuration));
            currentRotation++;
            StartCoroutine(rm.ChangeColor(currentDuration));
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
