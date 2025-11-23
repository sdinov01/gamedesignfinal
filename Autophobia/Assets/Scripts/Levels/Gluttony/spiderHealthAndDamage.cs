using UnityEngine;
using System.Collections;

public class spiderHealthAndDmg : MonoBehaviour
{
    private SpriteRenderer renderer;
    private spiderMovement movement;
    private healthBar health;
    private bool takeDamage;

    public float hitDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
        movement = this.GetComponent<spiderMovement>();
        health = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
        takeDamage = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /* Spider is vulnerable when red */
            if (renderer.color == Color.red)
            {
                Debug.Log("The spider is vulnerable and can take damage, and cannot deal damage\n");
                StartCoroutine(PlayerHit());
            }
            else
            {
                /* Player takes damage otherwise */
                health.takeDamage(2f);
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
                Debug.Log("SMACK!\n");
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
