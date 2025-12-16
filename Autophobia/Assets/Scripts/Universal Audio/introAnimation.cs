using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class introAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private levelTracker level;
    private static bool played = false;
    private string sceneName;


    // Update is called once per frame

    void Awake()
    {
        // Turn off Play On Awake in the inspector
        if (SceneManager.GetActiveScene().name == "Intro_Animation")
        {
            videoPlayer.playOnAwake = false;

            string path = Path.Combine(Application.streamingAssetsPath, "output.mp4");
            videoPlayer.url = path;

            videoPlayer.errorReceived += (player, msg) => Debug.LogError("VideoPlayer error: " + msg);
            videoPlayer.prepareCompleted += (player) =>
            {
                Debug.Log("Prepared, playing: " + videoPlayer.url);
                videoPlayer.Play();
            };

            Debug.Log("Preparing: " + videoPlayer.url);
            videoPlayer.Prepare();
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (videoPlayer.isPlaying && !played)
        {
            played = true;
        }
        if (played && !videoPlayer.isPlaying)
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
        yield return new WaitForSeconds(2f);
    }

}
