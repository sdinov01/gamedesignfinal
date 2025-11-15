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

   void Start() {
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
    }
}
