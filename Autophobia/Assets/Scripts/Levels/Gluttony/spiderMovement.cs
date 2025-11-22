//using UnityEngine;
//using System.Collections;

//public class spiderMovement : MonoBehaviour
//{
//    private Transform destination;
//    private Transform origin;

//    /* Decides whether the spider can move */
//    private static bool canMove = true;
//    private static bool canTakeDamage = false;
//    /* 1 by default */
//    private static float pulseDuration = 1f;
//    public float speed;
//    private int currentTime = 0;
//    private float timeElapsed;
//    private bool isPulsing = false;
//    private SpriteRenderer spriteRenderer;
//    private GameObject spawner;

//    public void SetDuration(float duration)
//    {
//        pulseDuration = duration;
//    }
//    void Start()
//    {
//        spawner = GameObject.FindWithTag("Spawner");
//        timeElapsed = 0f;
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }
//    // Update is called once per frame
//    void Update()
//    {

//        /* Move if the pulse is over */
//        if (canMove)
//        {
//            move();
//        } else if (!isPulsing)
//        {
//            StartCoroutine(pulse(pulseDuration));
//        }
//    }

//    private void move()
//    {
//        Vector3 destinationPos = new Vector3(destination.position.x, destination.position.y, 0f);
//        transform.position = Vector3.MoveTowards(transform.position, destinationPos, speed * Time.deltaTime);
//        if (transform.position == destinationPos)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private IEnumerator pulse(float pulseDur)
//    {
//        Color original = spriteRenderer.color;
//        isPulsing = true;
//        float elapsed = 0;
//        spriteRenderer.color = Color.red;
//        /* If the difference between the next pulse and this pulse < pulseDuration, then do not turn back to origina color until that is
//         * no longer true */
//        yield return new WaitForSeconds(pulseDur);

//        /* Wait until the pulse is over */
//        spriteRenderer.color = original;
//        canMove = true;
//        canTakeDamage = false;
//        isPulsing = false;

//    }

//    public void UpdateMove(bool move)
//    {
//        canMove = move;
//    }

//    public bool CanMove()
//    {
//        return canMove;
//    }

//    public void SetOriginAndDestination(Transform origin, Transform destination)
//    {
//        this.destination = destination;
//        this.origin = origin;
//        transform.position = new Vector3(origin.position.x, origin.position.y, 0f);
//    }
//}

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
