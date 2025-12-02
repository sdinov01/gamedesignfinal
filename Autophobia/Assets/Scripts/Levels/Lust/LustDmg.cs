using UnityEngine;
using System.Collections;

public class LustDMG : MonoBehaviour
{
    private Animator anim;
    private healthBar health;
    private cameraShake camShake;



    public float hitDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.GetComponent<Animator>();
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
