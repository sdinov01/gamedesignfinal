using UnityEngine;

public class handMovement : MonoBehaviour
{
    private bool isRotating = false;
    private float remainingRotation;   // how many degrees left
    private float speed;               // degrees per second (can be ±)

    public void PerformRotation(float targetRotation, float time)
    {
        // If time is 0, avoid divide-by-zero
        if (time <= 0f)
        {
            transform.Rotate(0, 0, targetRotation);
            return;
        }

        remainingRotation = targetRotation;
        speed = targetRotation / time;   // degrees per second
        isRotating = true;
    }

    void Update()
    {
        if (!isRotating) return;

        float delta = speed * Time.deltaTime;

        // If we would overshoot this frame, clamp to exactly the remainingRotation
        if (Mathf.Abs(delta) >= Mathf.Abs(remainingRotation))
        {
            transform.Rotate(0, 0, remainingRotation);
            isRotating = false;
        }
        else
        {
            transform.Rotate(0, 0, delta);
            remainingRotation -= delta;
        }
    }
}
