using UnityEngine;

public class RotateOnBeat : MonoBehaviour
{
    public bool clockwise;
    private float currentZ = 0f;

    void OnEnable()
    {
        BeatSync.OnBeat += RotateStep;
    }

    void OnDisable()
    {
        BeatSync.OnBeat -= RotateStep;
    }

    void RotateStep()
    {
        if (clockwise)
        {
            currentZ += 30f;
        }
        else
        {
            currentZ -= 30f;
        }
        currentZ %= 360f;  // wrap around cleanly

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentZ
        );
    }
}