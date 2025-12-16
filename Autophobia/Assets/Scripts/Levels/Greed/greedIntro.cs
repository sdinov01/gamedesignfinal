using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class greedIntro : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image tutorial;
    [SerializeField] private TMP_Text goodLuck;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text skip;
    [SerializeField] private spiderHealthAndDmg spiderHealth;
    [SerializeField] private Image timeBar;
    [SerializeField] private LustCountIn countin;

    private TimeBar timeBarFill;
    /* Delay before the song is played */
    public float musicDelay = 23f;
    public float fadeTime = 1.5f;
    public float fadeDelay = 11f;

    private Coroutine tutorialRoutine;
    private bool skipTutorial = false;

    void Start()
    {
        Debug.Log("Intro Start time = " + Time.timeSinceLevelLoad);
        timeBarFill = timeBar.GetComponent<TimeBar>();
        /* Default */
        musicDelay = 23f;
        skipTutorial = false;
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayDelayed(musicDelay);
        }
        tutorial.enabled = true;
        goodLuck.enabled = false;
        /* Start the tutorial messages */
        tutorialRoutine = StartCoroutine(TutorialMessage());
        if (timeBar != null)
        {
            Debug.Log("Time bar is not null");
            StartCoroutine(startBar());
        }
    }

    /* Make intro skippable */
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !skipTutorial)
        {
            skipTutorial = true;
            if (tutorialRoutine != null)
            {
                StopCoroutine(tutorialRoutine);
            }
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.Play();
            }
            // audioSource.Stop();
            // audioSource.Play();

            // Instantly hide tutorial & good luck, then fade background
            tutorial.enabled = false;

            goodLuck.color = new Color(goodLuck.color.r, goodLuck.color.g, goodLuck.color.b, 0);
            goodLuck.enabled = false;

            skip.color = new Color(skip.color.r, skip.color.g, skip.color.b, 0);
            skip.enabled = false;

            // Immediately fade background out
            background.enabled = false;
            musicDelay = Time.timeSinceLevelLoad;
            if (countin != null)
            {
                countin.enabled = true;
            }
        }
    }

    private IEnumerator TutorialMessage()
    {
        yield return StartCoroutine(FadeInImg(tutorial));

        yield return new WaitForSeconds(fadeDelay);
        if (skipTutorial) yield break;
        StartCoroutine(FadeOutImage(tutorial));
        yield return new WaitForSeconds(2.5f);
        if (skipTutorial) yield break;

        yield return StartCoroutine(FadeIn(goodLuck));
        yield return new WaitForSeconds(2f);
        if (skipTutorial) yield break;
        StartCoroutine(FadeOut(goodLuck));
        //background.gameObject.SetActive(false);
        if (skipTutorial) yield break;
        StartCoroutine(FadeOut(skip));
        StartCoroutine(FadeOutImage(background));
        yield return new WaitForSeconds(fadeTime);
        if (countin != null)
        {
            countin.enabled = true;
        }
        this.enabled = false;
    }

    private IEnumerator FadeInImg(Image text)
    {
        text.enabled = true;
        Color c = text.color;
        float t = 0;

        while (t < fadeTime)
        {
            if (skipTutorial) yield break;

            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 1);
    }

    private IEnumerator FadeIn(TMP_Text text)
    {
        text.enabled = true;
        Color c = text.color;
        float t = 0;

        while (t < fadeTime)
        {
            if (skipTutorial) yield break;

            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 1);
    }
    private IEnumerator FadeOutImage(Image background)
    {
        Color c = background.color;
        float startAlpha = c.a;
        float t = 0;

        while (t < fadeTime)
        {
            if (skipTutorial)
            {
                break;
            }
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
            if (skipTutorial)
            {
                break;
            }
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 0);
        text.enabled = false;
    }

    public bool SkippedTutorial()
    {
        return skipTutorial;
    }

    public float StartTime()
    {
        return musicDelay;
    }

    public IEnumerator startBar()
    {
        /* Begin after tutorial/skip */
        Debug.Log("musicDelay " + musicDelay);
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= musicDelay);

        /* Begin song Courotine fill bar */
        timeBarFill.SetDuration(audioSource.clip.length-15f);
        timeBarFill.BeginTime();
        Debug.Log("should start time bar");
    }
}
