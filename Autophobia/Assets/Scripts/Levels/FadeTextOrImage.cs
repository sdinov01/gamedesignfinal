using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FadeTextOrImage : MonoBehaviour
{
    private bool skipTutorial = false;

    public IEnumerator fadeImage(Image image,  float fadeTime)
    {
        Color c = image.color;
        float startAlpha = c.a;
        float timeElapsed = 0;
        while (timeElapsed < fadeTime)
        {
            if (skipTutorial)
            {
                yield break;
            }
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, timeElapsed / fadeTime);
            image.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;

        }
        image.enabled = false;
    }

    /* Use to fade text */
    public IEnumerator fadeText(TMP_Text text, float fadeTime)
    {
        Color c = text.color;
        float startAlpha = c.a;
        float timeElapsed = 0;
        while (timeElapsed < fadeTime)
        {
            if (skipTutorial)
            {
                yield break;
            }
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, timeElapsed / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;

        }
        text.enabled = false;
    }

    /* Use in separate script to update to skip the fade entirely */
    public void SkippedTutorial(bool skipTutorial)
    {
        this.skipTutorial = skipTutorial;
    }
}
