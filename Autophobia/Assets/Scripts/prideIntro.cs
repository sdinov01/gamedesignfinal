using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class prideIntro : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Image tutorial;
    [SerializeField] private Image background;
    [SerializeField] private TimeBar timeBar;
    [SerializeField] private TMP_Text skip;

    public CountIn countin;
    public LustCountIn lci;

    /* Default time will be 14.5 seconds after the scene is opened */
    private float beginTime = 14.5f;
    private float tutorialMSGDur = 10f;
    
    private float fadeDuration = 2f;

    public float fadeTime = 1.5f;
    public float fadeDelay = 11f;

    private Coroutine tutorialCoroutine;
    private bool skipTutorial = false;
    private bool musicStarted = false; 

    void Start()
    {
        /* Set the texts and image to be visible or invisible */
        tutorial.enabled = true;
        backgroundImage.enabled = true;
        /* Start the tutorial */
        tutorialCoroutine = StartCoroutine(TutorialMessage());

        if (timeBar != null)
        {
           StartCoroutine(startBar()); 
        }
    }

    private IEnumerator startBar()
    {
        /* Begin after tutorial/skip */
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= beginTime);

        /* Begin song Courotine fill bar */
        timeBar.SetDuration(audio.clip.length);
        timeBar.BeginTime();
        audio.Play();
    }

    void Update()
    {
        /* If left/right shift is pressed, skip tutorial */
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !skipTutorial && !PauseMenuHandler.GameisPaused)
        {
            /* Stop the tutorial coroutine */
            skipTutorial = true;

            if (tutorialCoroutine != null)
            {
                StopCoroutine(tutorialCoroutine);
            }

            /* Disable texts and background immediately */
            HideImageInstant(tutorial);
            HideTextInstant(skip);
            background.enabled = false;

        
            StartCoroutine(BeginCountIn());
            


            /* Update beginning time for time stamps later */
            beginTime = Time.timeSinceLevelLoad + 2f;
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

        StartCoroutine(FadeOut(skip));
        StartCoroutine(FadeOutImage(background));

        yield return new WaitForSeconds(fadeTime);
        if (skipTutorial) yield break;

    }

    private IEnumerator BeginCountIn()
    {
        Debug.Log("BEGAN");
        if (countin != null)
        {
            countin.enabled = true;
        }
        if (lci != null)
        {
            lci.enabled = true;
        }
        yield return null;
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

    public float StartTime()
    {
        return beginTime;
    }

}