using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class introAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private levelTracker level;
    private static bool played = false;
    private string sceneName;
    

    // Update is called once per frame

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Intro_Animation")
        {
            videoPlayer.Play();
            played = true;
        }
    }
    void Update()
    {
        if (played && !videoPlayer.isPlaying)
        {
            SceneManager.LoadScene("Level_Select_Scene");
        }
    }

    public bool HasPlayed()
    {
        return played;
    }
}
