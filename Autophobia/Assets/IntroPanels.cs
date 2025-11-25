using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class IntroPanels : MonoBehaviour
{
    public GameObject interactables;


    [Header("Info Panel 1")]
    public GameObject   T1Panel;
    public CanvasGroup  T1Group;
    public Button       T1Start;

    [Header("Info Panel 2")]
    public GameObject   T2Panel;
    public CanvasGroup  T2Group;
    public Button       T2Start;

    [Header("Misc")]
    public GameObject audiomanager;
    public LustCountIn lci;

    public float fadeInTime = 2f;


     void FirstButtonClicked()
    {
        Debug.Log ("First button clicked");
        StartCoroutine(FadeOutAndDeactivate(T1Panel, T1Group)); 
        CanvasHandler (T2Panel, T2Group, T2Start, false, true );       
    }

    private IEnumerator FadeOutAndDeactivate(GameObject g, CanvasGroup cg)
    {
        yield return StartCoroutine(FadeOutCanvas(cg));
        g.SetActive(false);
    }

    private void CanvasHandler (GameObject g, CanvasGroup cg, Button b, bool activate, bool callindex)
    {
        if (!activate) 
        {
            b.interactable = true;
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

        Button[] buttons = cg.GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }

    void SecondButtonClicked()
    {
        StartCoroutine(FadeOutAndDeactivate(T2Panel, T2Group));
        interactables.SetActive (true);
        if (lci != null)
        {
            lci.enabled = true;
        }
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanvasHandler (T1Panel, T1Group, T1Start, false, false );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
