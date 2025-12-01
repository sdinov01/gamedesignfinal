using UnityEngine;
using System.Collections;

public class SpiderHealth : MonoBehaviour
{
    private healthBar health;
    public float hitDuration;
    public float healAmt;

    void Start()
    {
        health = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerHit());
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
                health.healDamage(healAmt);
            }
            yield return null;
        }
    }
}
