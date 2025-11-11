using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelHandler : MonoBehaviour
{
    [SerializeField] private Button wrathButton;
    [SerializeField] private Button prideButton;
    [SerializeField] private Button slothButton;
    [SerializeField] private Button envyButton;
    [SerializeField] private Button greedButton;
    [SerializeField] private Button gluttonyButton;
    [SerializeField] private Button lustButton;
    void Start()
    {
        prideButton.interactable = false;
        slothButton.interactable = false;
        envyButton.interactable = false;
        greedButton.interactable = false;
        gluttonyButton.interactable = false;
        lustButton.interactable = false;
    }

    void Update()
    {
        /* If wrath is complete, sloth and envy are available */
        if (levelTracker.wrathComplete)
        {
            slothButton.interactable = true;
            envyButton.interactable = true;
        }
        /* If all three levels are complete, the next levels are playable */
        if (levelTracker.slothComplete && levelTracker.envyComplete)
        {
            prideButton.interactable = true;
            gluttonyButton.interactable = true;
            lustButton.interactable = true;
        }
        if (levelTracker.prideComplete && levelTracker.gluttonyComplete && levelTracker.lustComplete)
        {
            greedButton.interactable = true;
        }
    }
    /* Load the level with the name sceneName */
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
