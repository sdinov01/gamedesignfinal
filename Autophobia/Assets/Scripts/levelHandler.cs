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

    [SerializeField] private SpriteRenderer wrathSelect;
    [SerializeField] private SpriteRenderer prideSelect;
    [SerializeField] private SpriteRenderer slothSelect;
    [SerializeField] private SpriteRenderer envySelect;
    [SerializeField] private SpriteRenderer greedSelect;
    [SerializeField] private SpriteRenderer gluttonySelect;
    [SerializeField] private SpriteRenderer lustSelect;


    void Start()
    {
        /* If levels have been initialized already, return */
        if (levelTracker.IsEnabled())
        {
            return;
        }
        /* For now, keep all levels interactable */

        prideButton.interactable = false;
        slothButton.interactable = false;
        envyButton.interactable = false;
        greedButton.interactable = false;
        gluttonyButton.interactable = false;
        lustButton.interactable = false;

        gluttonySelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        prideSelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        slothSelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        envySelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        greedSelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        lustSelect.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }

    void Update()
    {
        /* If wrath is complete, sloth and greed are available */
        if (levelTracker.wrathComplete)
        {
            //Debug.Log("wrath complete");
            prideButton.interactable = true;
            greedButton.interactable = true;
            prideSelect.color = new Color(1f, 1f, 1f, 1f);
            greedSelect.color = new Color(1f, 1f, 1f, 1f);
        }
        /* If sloth and greed were completed, then pride, gluttony, and lust are available */
        if (levelTracker.prideComplete && levelTracker.greedComplete)
        {
            slothButton.interactable = true;
            lustButton.interactable = true;
            slothSelect.color = new Color(1f, 1f, 1f, 1f);
            lustSelect.color = new Color(1f, 1f, 1f, 1f);
        }
        /* When the previous three are completed, the final boss Envy is available */
        if (levelTracker.slothComplete && levelTracker.lustComplete)
        {
            gluttonyButton.interactable = true;
            gluttonySelect.color = new Color(1f, 1f, 1f, 1f);
        }
        if (levelTracker.gluttonyComplete) {
            envyButton.interactable = true;
            envySelect.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    /* Load the level with the name sceneName */
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
