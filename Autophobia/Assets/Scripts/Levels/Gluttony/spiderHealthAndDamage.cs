using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class spiderHealthAndDmg : MonoBehaviour
{
    private SpriteRenderer  renderer;
    private healthBar       health;
    private AudioSource     SFX;

    public float hitDuration;
    private string name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer    = this.GetComponent<SpriteRenderer>();
        health      = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
        name = SceneManager.GetActiveScene().name;
        if (name != "Envy_Level")
        {
            SFX = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /* Spider is vulnerable when red */
            if (renderer.color == Color.green)
            {
                StartCoroutine(PlayerHit());
            }
            else
            {
                /* Player takes damage otherwise */
                health.takeDamage(3.2f);
            }
        }
    }

    private IEnumerator PlayerHit()
    {
        float timeElapsed = 0f;
        while (timeElapsed < hitDuration)
        {
            timeElapsed += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                SFX.Play();
                if (name == "Envy_Level")
                {
                    health.healDamage(1.2f);
                }
                Destroy(gameObject);
            }
            yield return null;
        }
    }

}
