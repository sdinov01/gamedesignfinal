using UnityEngine;

public class LerpToTarget : MonoBehaviour
{
    public Transform target;
    public float lerpDuration = 0.5f;

    private Vector3 startPos;
    private float t;
    private bool moving;

    public void Begin(Transform dest, float duration)
    {
        if (dest == null)
        {
            Debug.LogError("Ball got NULL destination!", this);
            return;
        }

        target = dest;
        lerpDuration = duration;
        startPos = transform.position;
        t = 0f;
        moving = true;
    }

    void Update()
    {
        if (!moving) return;

        t += Time.deltaTime / lerpDuration;
        transform.position = Vector3.Lerp(startPos, target.position, t);

        if (t >= 1f)
            moving = false;
    }
}
