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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(winScene);
    }
}
