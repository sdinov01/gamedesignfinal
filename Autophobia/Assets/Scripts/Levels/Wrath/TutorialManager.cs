using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Initialization")]
    public GameObject playerbody;
    public Platform firstPlatform;
    public linearPlatMove player;
    public PlayerController controller;
    public float fadeInTime = 2f;
    public GameObject LevelManager;



    [Header("Tutorial Panel 1")]
    public GameObject   T1Panel;
    public CanvasGroup  T1Group;
    public Button       T1Start;



    [Header("Tutorial Panel 2")]
    public GameObject   T2Panel;
    public CanvasGroup  T2Group;
    public Button       T2Start;
    public GameObject audiomanager;



    [Header("Step 1 Move to the flashlight")]
    public GameObject[] glowObjects;  
    public GlowFlasher[] highlighters;
    int currentStep = 0;


    [Header("Music")]
    public GameObject audio;

    public KnifeController[] knives;

    public bool step2 = false;
    public bool startup = false;

    void Start() {
        LevelManager.SetActive(false);

        foreach (var g in glowObjects)
        {
            g.SetActive(false);   
        }
        player.transform.position = firstPlatform.transform.position;
        playerbody.SetActive (false);

        audio.SetActive(false);

        CanvasHandler (T1Panel, T1Group, T1Start, false, false);
    }

    void FirstButtonClicked()
    {
        StartCoroutine(FadeOutAndDeactivate(T1Panel, T1Group));
        playerbody.SetActive(true);
        controller.EnableMovement();
        
        glowObjects[0].SetActive(true);
        highlighters[0].StartFlashing();

    }

    void SecondButtonClicked()
    {
        StartStep2();
        StartCoroutine(FadeOutAndDeactivate(T2Panel, T2Group));
        audio.SetActive(true);
        RhythmManager rhythmManager = audiomanager.GetComponent<RhythmManager>();
        rhythmManager.Play();
        LevelManager.SetActive(true);

    }

    public IEnumerator WaitForSeconds (float s)
    {
        yield return new WaitForSeconds(s);
    }

    IEnumerator FadeInCanvas (CanvasGroup cg)
    {
        float elapsed = 0f;
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(elapsed / fadeInTime);
            yield return null;
        }
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    IEnumerator FadeOutCanvas (CanvasGroup cg)
    {
        float elapsed = 0f;
        float start = cg.alpha;

        while (elapsed < fadeInTime)
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, 0f, elapsed / fadeInTime);
            yield return null;
        }
        MakeInvisible (cg);
    }


    /* Second step*/
    public void PlayerReachedPlatform(int step)
    {
        Debug.Log("reach the plat");
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
            CanvasHandler (T2Panel, T2Group, T2Start, false, true); //show next canvas
        }
    }

    private IEnumerator FadeOutAndDeactivate(GameObject g, CanvasGroup cg)
    {
        yield return StartCoroutine(FadeOutCanvas(cg));
        g.SetActive(false);
    }

    public void StartStep2()
    {
        foreach (var k in knives)
        {
            k.canStart = true;
        }

    }

    private void CanvasHandler (GameObject g, CanvasGroup cg, Button b, bool activate, bool callindex)
    {
        if (!activate) 
        {
            b.onClick.RemoveAllListeners();
            if (callindex == false)
            {
                b.onClick.AddListener (FirstButtonClicked);
            } 
            else
            {
                b.onClick.AddListener (SecondButtonClicked);
            }
            MakeInvisible (cg);
            g.SetActive (true);
            StartCoroutine (FadeInCanvas (cg));
        }
        else
        {
            StartCoroutine (FadeOutAndDeactivate (g, cg));
        }
    }

    private void MakeInvisible (CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void CloseStep2Canvas()
    {
        // TPanel2.gameObject.SetActive(false);
    }
}
