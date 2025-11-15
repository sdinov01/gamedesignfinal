using UnityEngine;
using System.Collections;

public class PlatformHighlighter : MonoBehaviour
{
    public Light spotLight; 
    public float flashInterval = 0.1f;

    private bool flashing = false;

    public void StartFlashing()
    {
        if (!flashing)
        {
            flashing = true;
            StartCoroutine(Flash());
        }
    }

    public void StopFlashing()
    {
        flashing = false;
        spotLight.enabled = false;
    }

    IEnumerator Flash()
    {
        while (flashing)
        {
            spotLight.enabled = !spotLight.enabled; // 开关
            yield return new WaitForSeconds(flashInterval);
        }
    }
}