using UnityEngine;
using System.Collections;

public class spiderMovement : MonoBehaviour
{
    private Transform destination;
    private Transform origin;

    /* Decides whether the spider can move */
    private static bool canMove = true;
    private static bool canTakeDamage = false;
    public float pulseDuration;
    public float speed;
    private int currentTime = 0;
    private float timeElapsed;
    private bool isPulsing = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        timeElapsed = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
        /* Move if the pulse is over */
        if (canMove)
        {
            move();
        } else if (!isPulsing)
        {
            StartCoroutine(pulse());
        }
    }

    private void move()
    {
        Vector3 destinationPos = new Vector3(destination.position.x, destination.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, speed * Time.deltaTime);
        if (transform.position == destinationPos)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator pulse()
    {
        Color original = spriteRenderer.color;
        isPulsing = true;
        float elapsed = 0;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(pulseDuration);
        spriteRenderer.color = original;
        canMove = true;

        canTakeDamage = false;
        isPulsing = false;
    }

    public void UpdateMove(bool move)
    {
        canMove = move;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void SetOriginAndDestination(Transform origin, Transform destination)
    {
        this.destination = destination;
        this.origin = origin;
        transform.position = new Vector3(origin.position.x, origin.position.y, 0f);
    }
}

