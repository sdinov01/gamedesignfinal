using UnityEngine;
using System.Collections;

public class restrictMovement : MonoBehaviour
{
    [SerializeField] private healthBar health;
    [SerializeField] private Color red;
    [SerializeField] private cameraShake camShake;
    private bool inRed = false;
    private bool takeDamage = true;

    void OnCollisionStay2D(Collision2D collision)
    {
        /* If we are already taking damage, return so we don't take additional damage. */
        

        /* If the slice the player is inside is red, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == red);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* If the player is in a red slice, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == red);
        
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
        yield return new WaitForSeconds(0.25f);
        health.takeDamage(5f);
        yield return new WaitForSeconds(0.5f);
        takeDamage = true;
    }


}
