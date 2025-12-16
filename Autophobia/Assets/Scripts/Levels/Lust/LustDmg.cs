using UnityEngine;
using System.Collections;

public class LustDMG : MonoBehaviour
{
    private Animator anim;
    private healthBar health;
    private cameraShake camShake;

    private bool takingDamage = false;
    public float damageDuration;



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
        /* Prevents weird repetitive damage */
        if (takingDamage)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        /* Prevents weird repetitive damage */
        if (takingDamage)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        takingDamage = true;
        float elapsed = 0f;
        health.takeDamage(1.5f);
        camShake.SetShake(true);
        while (elapsed < damageDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        takingDamage = false;
    }

}
