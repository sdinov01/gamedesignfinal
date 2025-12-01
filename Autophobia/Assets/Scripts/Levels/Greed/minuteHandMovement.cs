using UnityEngine;
using System.Collections;

public class minuteHandMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationAmount = 15f; // degrees per frame or per key press
    private bool takingDamage = false;
    [SerializeField] private healthBar health;
    [SerializeField] private cameraShake camShake;
    private bool inRed = false;
    [SerializeField] private Color red;

    void OnCollisionStay2D(Collision2D collision)
    {
        /* If we are already taking damage, return so we don't take additional damage. */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == red);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* If the player is in a red slice, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        inRed = (slice.material.GetColor("_Color") == red);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationAmount * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -rotationAmount * Time.deltaTime);
        }
        if (inRed && !takingDamage)
        {
            StartCoroutine(Damage());
        }
    }

    private IEnumerator Damage()
    {
        takingDamage = true;
        camShake.SetShake(true);
        health.takeDamage(7.5f);
        yield return new WaitForSeconds(0.8f);
        takingDamage = false;
    }
}
