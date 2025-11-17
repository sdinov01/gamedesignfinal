using UnityEngine;

public class restrictMovement : MonoBehaviour
{
    [SerializeField] private healthBar health;
    [SerializeField] private Color red;
    private bool takeDamage = false;

    void OnCollisionStay2D(Collision2D collision)
    {
        /* If we are already taking damage, return so we don't take additional damage. */
        if (takeDamage)
        {
            return;
        }

        /* If the slice the player is inside is red, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        if (slice.material.GetColor("_Color") == red)
        {
            takeDamage = true;

        }
        else
        {
            takeDamage = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* If the player is in a red slice, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        if (slice.material.GetColor("_Color") == red)
        {
            takeDamage = true;

        } else
        {
            takeDamage = false;
        }
    }

    void Update()
    {
        /* Take damage when this is true */
        if (takeDamage)
        {
            health.takeDamage(0.05f);
        }
    }


}
