using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (dm.isDialogueFinished()) {
            SceneManager.LoadScene(levelName);
        }
    }
}
