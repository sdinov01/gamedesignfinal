using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class gluttonyIntro : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text tutorial;
    [SerializeField] private TMP_Text goodLuck;
    [SerializeField] private Image background;
    /* Delay before the song is played */
    public float musicDelay = 15f;
    public float fadeTime = 5f;
    public float fadeDelay = 11f;

    void Start()
    {
        audioSource.PlayDelayed(musicDelay);
        tutorial.enabled = true;
        goodLuck.enabled = false;
        /* Start the tutorial messages */
        StartCoroutine(TutorialMessage(fadeDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TutorialMessage(float fadeDelay)
    {
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine(FadeOut(tutorial));
        yield return new WaitForSeconds(2.5f);
        goodLuck.enabled = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut(goodLuck));
        StartCoroutine(FadeOutImage(background));
        yield return new WaitForSeconds(fadeTime);
    }

    private IEnumerator FadeOutImage(Image background)
    {
        Color c = background.color;
        float startAlpha = c.a;
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeTime);
            background.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        background.color = new Color(c.r, c.g, c.b, 0);
        background.enabled = false;
    }
    private IEnumerator FadeOut(TMP_Text text)
    {
        Color c = text.color;
        float startAlpha = c.a;
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 0);
        text.enabled = false;
    }
}
