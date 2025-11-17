using UnityEngine;

public class restrictMovement : MonoBehaviour
{
    [SerializeField] private healthBar health;
    [SerializeField] private Color red;

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* If the slice the player is inside is red, take damage */
        Renderer slice = collision.gameObject.GetComponent<Renderer>();
        if (slice.material.GetColor("_Color") == red)
        {
            health.takeDamage(2f);
        }
    }


}
