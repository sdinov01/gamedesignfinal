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
    //[SerializeField] private Image fillAmount;
    //[SerializeField] private timeBar time;
    private Coroutine performRotationCoroutine;

    void Start()
    {
        performRotationCoroutine = StartCoroutine(hourMovement.performRotations());
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.time >= audio.clip.length)
        {
            levelTracker.greedComplete = true;
        }

    }
}
