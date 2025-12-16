using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    private bool start = false;
    private float duration;
    public string winScene = "";
    void Start()
    {
        progressBar.fillAmount = 0;   
    }

    void Update()
    {
        if (start)
        {
            StartCoroutine(startTime());
            start = false;
        }
        if (progressBar.fillAmount == 1f)
        {
            StartCoroutine(endScene());
        }
    }

    public void BeginTime()
    {
        start = true;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    private IEnumerator startTime()
    {
        float timeElapsed = 0f;
        while (timeElapsed < this.duration)
        {
            timeElapsed += Time.deltaTime;
            progressBar.fillAmount = timeElapsed / duration;
            yield return null;
        }
        progressBar.fillAmount = 1f;
    }

    private IEnumerator endScene()
    {   
        switch (winScene)
        {
            case "wrath_end_dialogue":
                Debug.Log("Wrath Complete");
                levelTracker.wrathComplete = true;
                break;
            case "pride_end_dialogue":
                Debug.Log("pride Complete");
                levelTracker.prideComplete = true;
                break;
            case "Gluttony_end_dialogue":
                Debug.Log("gluttony Complete");
                levelTracker.gluttonyComplete = true;
                break;
            case "lust_end_dialogue":
                Debug.Log("lust Complete");
                levelTracker.lustComplete = true;
                break;
            case "sloth_end_dialogue":
                Debug.Log("sloth Complete");
                levelTracker.slothComplete = true;
                break;
            case "greed_end_dialogue":
                Debug.Log("greed Complete");
                levelTracker.greedComplete = true;
                break;
            case "envy_end_dialogue":
                Debug.Log("envy Complete");
                levelTracker.envyComplete = true;
                break;
        }
       
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(winScene);
    }
}
