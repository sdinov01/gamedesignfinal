using UnityEngine;
using System.Collections;

public class LustDMG : MonoBehaviour
{
    private SpriteRenderer renderer;
    private healthBar health;
    private cameraShake camShake;

    public float hitDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
        health = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
        camShake = GameObject.FindWithTag("MainCamera").GetComponent<cameraShake>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            health.takeDamage(1.5f);
            camShake.SetShake(true);
        }
    }

}
