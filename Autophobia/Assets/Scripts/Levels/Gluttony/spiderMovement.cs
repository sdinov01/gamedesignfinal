using UnityEngine;
using System.Collections;

public class spiderMovement : MonoBehaviour
{
    private Transform destination;
    private Transform origin;

    /* Decides whether the spider can move */
    private bool canMove = false;
    private bool canTakeDamage = false;
    public float pulseDuration;
    public float speed;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            move();
        } else
        {
            /* If it cannot move, pulse and become vulnerable */
            canTakeDamage = true;
            StartCoroutine(pulse(pulseDuration));
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

    private IEnumerator pulse(float pulseDur)
    {
        float elapsed = 0;
        while (elapsed < pulseDur)
        {
            elapsed += Time.deltaTime;
        }
        yield return null;
    }

    public void UpdateMove(bool move)
    {
        canMove = move;
    }

    public void SetOriginAndDestination(Transform origin, Transform destination)
    {
        this.destination = destination;
        this.origin = origin;
        transform.position = new Vector3(origin.position.x, origin.position.y, 0f);
    }
}
