using UnityEngine;
using System.Collections;

public class GlowFlasher : MonoBehaviour
{
    public SpriteRenderer sr;          // 拖入 SpriteRenderer
    public float pulseSpeed = 2f;      //bigger ->faster
    public float minAlpha = 0.1f;      // darkest alpha
    public float maxAlpha = 0.9f;      // lightest alpha
    public float minScale = 0.8f;      
    public float maxScale = 1.15f;     

    bool flashing = false;
    Coroutine flashCoroutine;

    public void StartFlashing()
    {
        if (flashing) return;
        flashing = true;
        flashCoroutine = StartCoroutine(Pulse());
    }

    public void StopFlashing()
    {
        flashing = false;
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        SetAlpha(0f);
        transform.localScale = Vector3.one * minScale;
    }

    IEnumerator Pulse()
    {
        float t = 0f;
        while (flashing)
        {
            t += Time.deltaTime * pulseSpeed;
            float s = (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f;
            float a = Mathf.Lerp(minAlpha, maxAlpha, s);
            float sc = Mathf.Lerp(minScale, maxScale, s);

            SetAlpha(a);
            transform.localScale = Vector3.one * sc;

            yield return null;
        }
    }

    void SetAlpha(float a)
    {
        if (sr == null) return;
        Color c = sr.color;
        c.a = Mathf.Clamp01(a);
        sr.color = c;
    }

    // （可选）编辑器里也看到效果
    // #if UNITY_EDITOR
    // void OnValidate()
    // {
    //     if (sr != null)
    //     {
    //         Color c = sr.color;
    //         c.a = minAlpha;
    //         sr.color = c;
    //         transform.localScale = Vector3.one * minScale;
    //     }
    // }
    //#endif
}