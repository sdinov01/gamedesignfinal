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

    [Header("Step 2 Rhythm Dodge")]
    public CanvasGroup canvasGroup2;
    public Button step2Button;

    [Header("Music")]
    public AudioSource audioSource;
    public KnifeController[] knives;




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
    step2Button.onClick.AddListener(CloseStep2Canvas);

    StartCoroutine(FadeInCanvas());

    }

    IEnumerator FadeInCanvas()
    {
        canvasGroup2.gameObject.SetActive(false);
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
        Debug.Log("currentStep:" + currentStep);

        if (currentStep < highlighters.Length){
            glowObjects[currentStep].SetActive(true);
            highlighters[currentStep].StartFlashing();
        }

        else
        {
            canvasGroup2.gameObject.SetActive(true);
            StartCoroutine(FadeInCanvasAfterStep()); //show next canvas
        }
    }


    /*STEP 2*/
    IEnumerator FadeInCanvasAfterStep()
    {
        canvasGroup2.alpha = 0;
        canvasGroup2.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup2.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup2.interactable = true;
        canvasGroup2.blocksRaycasts = true;

        StartStep2(); // Step 2 开始
    }

    public void StartStep2()
    {
        audioSource.Play();
        foreach (var k in knives)
        {
            k.canStart = true;
        }

    }

    public void CloseStep2Canvas()
    {
        canvasGroup2.gameObject.SetActive(false);
    }
}
