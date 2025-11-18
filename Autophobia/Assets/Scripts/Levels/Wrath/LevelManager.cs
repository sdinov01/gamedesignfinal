using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public healthBar playerHealth; 
    public AudioSource music;  
    private bool levelCompleted = false;
    public TutorialManager t;

    void Update()
    {
        if (!music.isPlaying && playerHealth.healthLeft() > 0 && t.step2)
        {
            levelCompleted = true;
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        // UI for survive
        Debug.Log("Level Complete!");
        SceneManager.LoadScene("Level_Select_Scene");
    }
}