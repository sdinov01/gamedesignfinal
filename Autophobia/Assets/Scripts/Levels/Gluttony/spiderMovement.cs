using UnityEngine;
using System.Collections;

public class spiderMovement : MonoBehaviour
{
    private Transform destination;
    private Transform origin;

    public float speed;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 🔹 Global pulse state (shared by all spiders)
    private static bool isPulsingGlobal = false;
    private static float pulseEndTime = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // If a global pulse is active
        if (isPulsingGlobal)
        {
            // Turn red and don't move
            spriteRenderer.color = Color.red;

            // End of pulse?
            if (Time.time >= pulseEndTime)
            {
                isPulsingGlobal = false;
                spriteRenderer.color = originalColor;
            }
        }
        else
        {
            // Normal behavior: move
            spriteRenderer.color = originalColor;
            Move();
        }
    }

    private void Move()
    {
        if (destination == null) return;

        Vector3 destinationPos = new Vector3(destination.position.x, destination.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, speed * Time.deltaTime);

        if (transform.position == destinationPos)
        {
            Destroy(gameObject);
        }
    }

    // 🔹 Called by spawner to trigger a global pulse
    public static void TriggerPulse(float duration)
    {
        isPulsingGlobal = true;
        pulseEndTime = Time.time + duration;
    }

    public void SetOriginAndDestination(Transform origin, Transform destination)
    {
        this.destination = destination;
        this.origin = origin;
        transform.position = new Vector3(origin.position.x, origin.position.y, 0f);
    }
}
