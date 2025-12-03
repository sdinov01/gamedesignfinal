using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class slothCountin : MonoBehaviour
{
    public TextMeshProUGUI countInText;
    public bool IsSloth;
    public Light2D slothCenterLight;     
    public Light2D[] Lights1;
    public Light2D[] Lights2;
    public AudioSource music;
    //public TimeBar tb;

    
    void Start()
    {
        StartCoroutine (Countdown ("3", "2", "1", "GO!"));
    }

    IEnumerator Countdown (string s3, string s2, string s1, string go)
    {
        // interactables.SetActive (false);
        countInText.text = s3;
        slothCenterLight(3);
        yield return new WaitForSeconds (1.2f);

        countInText.text = s2;
        slothCenterLight(2);
        yield return new WaitForSeconds (1.2f);

        countInText.text = s1;
        slothCenterLight(1);
        yield return new WaitForSeconds (1.2f);

        countInText.text = go;
        slothCenterLight(0);
        yield return new WaitForSeconds (1.2f);

        ActivateAll();

    }

    void SlothLight(int stage)
    {
        if(!IsSloth){return;}
        slothCenterLight.SetActive(false);
        foreach (var g in Light1)
        {
            g.SetActive(false);
        }
        foreach (var g in Light2)
        {
            g.SetActive(false);
        }
            
        if (stage == 3)
        {
            slothCenterLight.SetActive(true);
        }

        if (stage == 2)
        {
            foreach (var g in Light1)
            {
                g.SetActive(true);
            }
        }
        if (stage == 1)
        {
            foreach (var g in Light2)
            {
                g.SetActive(true);
            }
        }
    }

    
    void ActivateAll()
    {
        music.Play();
    }


   
}

