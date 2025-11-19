using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float duration = 0.5f;
    private bool start;
    public AnimationCurve curve;
    
    public void SetShake(bool shake)
    {
        start = shake;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    private IEnumerator Shaking()
    {
        float timeElapsed = 0f;
        Vector3 startPos = transform.position;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float strength = curve.Evaluate(timeElapsed / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
    }
}
