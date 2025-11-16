using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
   public Platform firstPlatform;
   public GameObject TutorialPanel;
   public Button startButton;
   public platformMovement player;
   public PlayerController controller;
   public CanvasGroup canvasGroup;
   public float fadeDuration = 1f;

   [Header("Step 1 Move to the flashlight")]
   public GameObject[] glowObjects;  
    public GlowFlasher[] highlighters;
    int currentStep = 0;


   void Start() {
    foreach (var g in glowObjects)
        {
            g.SetActive(false);   
        }
    player.transform.position = firstPlatform.transform.position;

    //initialize canvasGroup
    canvasGroup.alpha = 0;
    canvasGroup.interactable = false;
    canvasGroup.blocksRaycasts = false;

    startButton.onClick.AddListener(ButtonClicked);

    StartCoroutine(FadeInCanvas());
    }

    IEnumerator FadeInCanvas()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void ButtonClicked()
    {
        //after clicking, canvas disappear
        TutorialPanel.SetActive(false);
        //player can move now
        controller.EnableMovement();

        glowObjects[0].SetActive(true);
        highlighters[0].StartFlashing();
    }


    /* Second step*/
    public void PlayerReachedPlatform(int step)
    {
        if (step != currentStep) return; 

        highlighters[step].StopFlashing();

        currentStep++;

        if (currentStep < highlighters.Length){
            glowObjects[currentStep].SetActive(true);
            highlighters[currentStep].StartFlashing();
        }

        else
        {
            StartCoroutine(FadeInCanvasAfterStep()); //show next canvas
        }
    }

    IEnumerator FadeInCanvasAfterStep()
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
