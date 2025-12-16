using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueLoader : MonoBehaviour
{
    public string levelName = "";
    public string winSceneName = "";
    public DialogueManager dm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dm != null) {
            if (dm.isDialogueFinished()) {
                SceneManager.LoadScene(levelName);
            }
        }

        if (winSceneName != "") {
            Debug.Log(winSceneName);
            switch (winSceneName)
            {
                case "wrath_end_dialogue":
                    if (levelTracker.wrathComplete) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "pride_end_dialogue":
                    if (levelTracker.prideComplete) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "Gluttony_end_dialogue":
                    if (levelTracker.gluttonyComplete) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "lust_end_dialogue":
                    if (levelTracker.lustComplete) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "sloth_end_dialogue":
                    if (levelTracker.slothComplete) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "greed_end_dialogue":
                    //Debug.Log("inside correct case");
                    if (levelTracker.greedComplete) {
                        //Debug.Log("Scene loads");
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "envy_end_dialogue":
                    if (levelTracker.envyComplete) {
                        Debug.Log("Scene loads");
                        SceneManager.LoadScene("End_Scene");
                    }
                    break;
            }
        }
    }

}
