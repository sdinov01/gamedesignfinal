using UnityEngine;
using System.Collections;

public class spiderHealthAndDmg : MonoBehaviour
{
    private SpriteRenderer renderer;
    private spiderMovement movement;
    private healthBar health;
    private bool takeDamage;
    private static bool canHeal = false;

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
                StartCoroutine(PlayerHit());
            }
            else
            {
                /* Player takes damage otherwise */
                health.takeDamage(1.6f);
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
                Destroy(gameObject);
                /* Player will heal */
                if (canHeal)
                {
                    health.healDamage(1f);
                }
            }
            yield return null;
        }
    }

    public void CanHeal(bool heal)
    {
        canHeal = heal;
    }
}
