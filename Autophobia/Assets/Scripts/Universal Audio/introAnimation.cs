using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class introAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    private static bool played = false;
    private string sceneName;
    [SerializeField] private TMP_Text skip;

    private FadeTextOrImage fadeFunction;


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Intro_Animation")
        {
            /* Retrieve .mp4 for intro animation and play it */
            videoPlayer.playOnAwake = false;

            string path = Path.Combine(Application.streamingAssetsPath, "output.mp4");
            videoPlayer.url = path;

            videoPlayer.errorReceived += (player, msg) => Debug.LogError("VideoPlayer error: " + msg);
            videoPlayer.prepareCompleted += (player) =>
            {
                videoPlayer.Play();
            };

            videoPlayer.Prepare();
        }
    }

    void Start()
    {
        fadeFunction = new FadeTextOrImage();
        StartCoroutine(Delay());

    }
    void Update()
    {
        if (videoPlayer.isPlaying && !played)
        {
            played = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene("Level_Select_Scene");
        }
    }

    public bool HasPlayed()
    {
        return played;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(fadeFunction.fadeText(skip, 2f));
    }

}
