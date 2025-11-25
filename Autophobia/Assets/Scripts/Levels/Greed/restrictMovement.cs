using UnityEngine;
using System.Collections;

public class restrictMovement : MonoBehaviour
{
    /* Handles color change */
    [SerializeField] private Color[] colors;
    [SerializeField] private float[] colorDuration;
    [SerializeField] private float[] changeDuration;
    private int durationIndex = 0;

    /* Handles whether to change color */
    [SerializeField] private handMovement hourHand;
    private bool inRed = false;
    private bool takeDamage = true;
    private GameObject currentSlice;

    /* Handles effects */
    [SerializeField] private healthBar health;
    [SerializeField] private cameraShake camShake;
    [SerializeField] private AudioSource audio;

    void OnCollisionStay2D(Collision2D collision)
    {
        /* If we are already taking damage, return so we don't take additional damage. */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == colors[2]);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* If the player is in a red slice, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == colors[2]);
    }

    void Update()
    {
        /* Duration of the color change has to be updated */
        if (audio.time >= changeDuration[durationIndex] && durationIndex < changeDuration.Length - 1)
        {
            durationIndex++;
        }
    }
    void FixedUpdate()
    {
        /* Take damage when this is true */
        if (takeDamage && inRed)
        {
            StartCoroutine(damage());
        }
    }

    private IEnumerator damage()
    {
        takeDamage = false;
        camShake.SetShake(true);
        health.takeDamage(5f);
        yield return new WaitForSeconds(0.8f);
        takeDamage = true;
    }

    /* Change color of slice */
    public IEnumerator ChangeColor(float time)
    {
        /* Wait to finish rotation */
        yield return new WaitForSeconds(time);
        /* Retrieve the slice to change the color of */
        currentSlice = hourHand.GetCurrentSlice();
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

}
