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

    private Coroutine tutorialRoutine;
    private bool skipTutorial = false;

    void Start()
    {
        audioSource.PlayDelayed(musicDelay);
        tutorial.enabled = true;
        goodLuck.enabled = false;
        /* Start the tutorial messages */
        tutorialRoutine = StartCoroutine(TutorialMessage());
    }

    /* Make intro skippable */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            skipTutorial = true;
            if (tutorialRoutine != null)
            {
                StopCoroutine(tutorialRoutine);
            }
            audioSource.Stop();   // cancels scheduled/playing clip
            audioSource.Play();   // start right now

            // Instantly hide tutorial & good luck, then fade background
            tutorial.color = new Color(tutorial.color.r, tutorial.color.g, tutorial.color.b, 0);
            tutorial.enabled = false;

            goodLuck.color = new Color(goodLuck.color.r, goodLuck.color.g, goodLuck.color.b, 0);
            goodLuck.enabled = false;

            // Immediately fade background out
            background.enabled = false;
        }
    }

    private IEnumerator TutorialMessage()
    {
        yield return new WaitForSeconds(fadeDelay);
        if (skipTutorial) yield break;
        StartCoroutine(FadeOut(tutorial));
        yield return new WaitForSeconds(2.5f);
        if (skipTutorial) yield break;
        goodLuck.enabled = true;
        yield return new WaitForSeconds(2f);
        if (skipTutorial) yield break;
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
}
