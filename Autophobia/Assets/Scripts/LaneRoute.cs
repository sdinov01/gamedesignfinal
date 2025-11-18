using UnityEngine;

public class LaneRoute : MonoBehaviour
{
    public Transform destination;
    public float speed = 4f;

    void Update()
    {
        if (destination == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destination.position,
            speed * Time.deltaTime
        );
    }
}
