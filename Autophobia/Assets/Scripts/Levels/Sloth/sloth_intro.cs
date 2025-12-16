using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SlothIntro: MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image tutorial;
    [SerializeField] private TMP_Text goodLuck;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text skip;
    [SerializeField] private Image timeBar;
    [SerializeField] private count_down countdown;

    private TimeBar timeBarFill;

    public float fadeTime = 1.5f;
    public float fadeDelay = 11f;

    private Coroutine tutorialRoutine;
    private bool skipTutorial = false;
    private bool musicStarted = false;  //music start when tutorial end

    void Start()
    {
        timeBarFill = timeBar.GetComponent<TimeBar>();

        skipTutorial = false;

        if (audioSource != null)
        {
            audioSource.Stop(); // stop audio source at the during tutorial
        }

        tutorial.enabled = true;
        goodLuck.enabled = false;

        tutorialRoutine = StartCoroutine(TutorialMessage());

        if (timeBar != null)
        {
            StartCoroutine(startBar());
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !skipTutorial && !PauseMenuHandler.GameisPaused)
        {
            skipTutorial = true;

            if (tutorialRoutine != null)
            {
                StopCoroutine(tutorialRoutine);
            }

            // UI disappear immediately when skip
            //HideTextInstant(tutorial);
            HideImageInstant(tutorial);
            HideTextInstant(goodLuck);
            HideTextInstant(skip);
            background.enabled = false;

           countdown.StartCountdown(); // start count down when skip
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
        StartCoroutine(FadeOut(skip));
        StartCoroutine(FadeOutImage(background));

        yield return new WaitForSeconds(fadeTime);
        if (skipTutorial) yield break;

        countdown.StartCountdown();//start count down when tutorial end
    }


    public IEnumerator startBar()
    {
        yield return new WaitUntil(() => audioSource.isPlaying);

        timeBarFill.SetDuration(audioSource.clip.length);
        timeBarFill.BeginTime();
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

    private IEnumerator FadeOut(TMP_Text text)
    {
        Color c = text.color;
        float startAlpha = c.a;
        float t = 0;

        while (t < fadeTime)
        {
            if (skipTutorial) break;

            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeTime);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        text.color = new Color(c.r, c.g, c.b, 0);
        text.enabled = false;
    }

    private IEnumerator FadeOutImage(Image img)
    {
        Color c = img.color;
        float startAlpha = c.a;
        float t = 0;

        while (t < fadeTime)
        {
            if (skipTutorial) break;

            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, t / fadeTime);
            img.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        img.color = new Color(c.r, c.g, c.b, 0);
        img.enabled = false;
    }

    void HideTextInstant(TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.enabled = false;
    }

    void HideImageInstant(Image img)
    {
        img.enabled = false;
    }
}