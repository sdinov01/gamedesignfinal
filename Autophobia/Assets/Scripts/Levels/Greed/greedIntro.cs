using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class greedIntro : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private AudioSource audio;
    [SerializeField] private TMP_Text tutorial;
    [SerializeField] private TMP_Text goodLuck;
    [SerializeField] private TimeBar timeBar;

    /* Default time will be 14.5 seconds after the scene is opened */
    private float beginTime = 14.5f;
    private float tutorialMSGDur = 7f;
    private Coroutine tutorialCoroutine;
    private bool skipTutorial = false;
    private float fadeDuration = 2f;

    void Start()
    {
        /* Set the audio source to play at the beginning time */
        audio.PlayDelayed(beginTime);
        /* Set duration */
        timeBar.SetDuration(audio.clip.length);
        /* Set the texts and image to be visible or invisible */
        tutorial.enabled = true;
        goodLuck.enabled = false;
        backgroundImage.enabled = true;
        /* Start the tutorial */
        tutorialCoroutine = StartCoroutine(handleTutorial());
        StartCoroutine(startBar());
    }

    private IEnumerator startBar()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.time >= beginTime);

        /* Begin song Courotine fill bar */
        timeBar.SetDuration(audio.clip.length);
        timeBar.BeginTime();
    }

    void Update()
    {
        /* If left/right shift is pressed, skip tutorial */
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            /* Stop the tutorial coroutine */
            skipTutorial = true;
            StopCoroutine(tutorialCoroutine);

            /* Resets music to start now */
            audio.Stop();
            audio.Play();

            /* Disable texts and background immediately */
            tutorial.enabled = false;
            goodLuck.enabled = false;
            backgroundImage.enabled = false;

            /* Update beginning time for time stamps later */
            beginTime = Time.time;

            /* Start progress bar */
            //timeBar.BeginTime();
        }   
    }

    private IEnumerator handleTutorial()
    {
        if (skipTutorial)
        {
            yield break;
        }
        /* Enable tutorial screen for tutorialMSGdur seconds */
        tutorial.enabled = true;
        yield return new WaitForSeconds(tutorialMSGDur);
        if (skipTutorial)
        {
            yield break;
        }
        /* Fade the text away */
        StartCoroutine(fadeText(tutorial, fadeDuration));
        if (skipTutorial)
        {
            yield break;
        }

        /* Enable good luck message for half the duration of the tutorial */
        yield return new WaitForSeconds(fadeDuration);
        if (skipTutorial)
        {
            yield break;
        }
        goodLuck.enabled = true;
        yield return new WaitForSeconds(tutorialMSGDur / 2);

        if (skipTutorial)
        {
            yield break;
        }
        /* Fade the good luck text and background */
        StartCoroutine(fadeImage(backgroundImage, fadeDuration));
        StartCoroutine(fadeText(goodLuck, fadeDuration));
    }

    private IEnumerator fadeImage(Image image, float duration)
    {
        Color c = image.color;
        float startAlpha = c.a;
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            if (skipTutorial)
            {
                yield break;
            }
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, timeElapsed / duration);
            image.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;

        }
        image.enabled = false;
    }

    private IEnumerator fadeText(TMP_Text text, float duration)
    {
        Color c = text.color;
        float startAlpha = c.a;
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            if (skipTutorial)
            {
                yield break;
            }
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, timeElapsed / duration);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;

        }
        text.enabled = false;
    }

    public float StartTime()
    {
        return beginTime;
    }
}
