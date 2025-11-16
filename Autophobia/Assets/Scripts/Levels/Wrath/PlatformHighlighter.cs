using UnityEngine;
using System.Collections;

public class PlatformHighlighter : MonoBehaviour
{
    public Light spotLight;
    public float flashInterval = 0.3f;
    Coroutine flashing;

    public void StartFlashing()
    {
        if (flashing != null) StopCoroutine(flashing);
        flashing = StartCoroutine(Flash());
    }

    public void StopFlashing()
    {
        if (flashing != null) StopCoroutine(flashing);
        spotLight.enabled = false;
    }

    IEnumerator Flash()
    {
        while (true)
        {
            spotLight.enabled = !spotLight.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}