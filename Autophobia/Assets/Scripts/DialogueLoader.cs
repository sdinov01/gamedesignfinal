using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueLoader : MonoBehaviour
{
    public string levelName = "";
    public string winSceneName = "";
    public DialogueManager dm;
    public levelTracker lt;

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
            switch (winSceneName)
            {
                case "wrath_end_dialogue":
                    if (lt.isWrathComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "pride_end_dialogue":
                    if (lt.isPrideComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "Gluttony_end_dialogue":
                    if (lt.isGluttonyComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "lust_end_dialogue":
                    if (lt.isLustComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "sloth_end_dialogue":
                    if (lt.isSlothComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "greed_end_dialogue":
                    if (lt.isGreedComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
                case "envy_end_dialogue":
                    if (lt.isEnvyComplete()) {
                        SceneManager.LoadScene(winSceneName);
                    }
                    break;
            }
        }
    }

}
