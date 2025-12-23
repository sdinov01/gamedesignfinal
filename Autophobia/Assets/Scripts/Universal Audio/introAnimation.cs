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
    private float duration = 203f;

    private FadeTextOrImage fadeFunction;
    [SerializeField] private PauseMenuHandler pause;


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Intro_Animation")
        {
            /* Retrieve .mp4 for intro animation and play it through URL */
            videoPlayer.playOnAwake = false;

            string path = Path.Combine(Application.streamingAssetsPath, "animation.mp4");
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
        //fadeFunction = new FadeTextOrImage();
        fadeFunction = GetComponent<FadeTextOrImage>();
        if (skip != null)
        {
            StartCoroutine(Delay());
        }

    }
    void Update()
    {
        /* If the game is paused, pause the animation */
        
        if (SceneManager.GetActiveScene().name != "Intro_Animation")
        {
            return;   
        }
        if (pause != null && pause.IsPaused())
        {
            videoPlayer.Pause();
        }
        else if (pause != null && !pause.IsPaused()) {
            videoPlayer.Play();
        }

        /* If the video has finished playing, load level select scene */
        if (videoPlayer.frame >= (long)videoPlayer.frameCount - 1 && videoPlayer.frame > 0)
        {
            Debug.Log(videoPlayer.frame + " " + (videoPlayer.frameCount - 1));
            Debug.Log("Finished animation!");
            StartCoroutine(FinishScene());
        }

        /* Allows player to skip cutscene */
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

    private IEnumerator FinishScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level_Select_Scene");
    }

}
