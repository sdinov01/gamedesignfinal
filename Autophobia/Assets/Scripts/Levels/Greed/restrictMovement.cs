using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class restrictMovement : MonoBehaviour
{
    /* Color behavior */
    [SerializeField] private Color[] colors;
    [SerializeField] private float[] colorDuration;
    [SerializeField] private float[] changeDuration;
    private int durationIndex = 0;

    /* Slices touched */
    private Dictionary<GameObject, Coroutine> activeCoroutines = new Dictionary<GameObject, Coroutine>();

    /* Interaction with hand */
    [SerializeField] private handMovement hourHand;

    /* Damage system */
    [SerializeField] private AudioSource audio;
    private bool takeDamage = true;


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject slice = collision.gameObject;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject slice = collision.gameObject;

        /* If we are able to change color */
        if (hourHand.CanChangeColor())
        {
            /* Check if the slice is already changing color. If so, stop that coroutine */
            if (activeCoroutines.ContainsKey(slice))
            {
                StopCoroutine(activeCoroutines[slice]);
                activeCoroutines.Remove(slice);
            }

            /* Start new color animation */
            Coroutine co = StartCoroutine(ChangeColor(slice));
            activeCoroutines[slice] = co;
        }
    }


    void Update()
    {
        if (durationIndex < changeDuration.Length - 1 && audio.time >= changeDuration[durationIndex])
        {
            durationIndex++;
        }
    }

    public IEnumerator ChangeColor(GameObject slice)
    {
        if (slice == null) yield break;

        Renderer rend = slice.GetComponent<Renderer>();
        if (rend == null) yield break;

        float step = colorDuration[durationIndex];

        /* Change color of slice */
        for (int i = 0; i < colors.Length; i++)
        {
            rend.material.SetColor("_Color", colors[i]);
            yield return new WaitForSeconds(step);
        }
    }
}
