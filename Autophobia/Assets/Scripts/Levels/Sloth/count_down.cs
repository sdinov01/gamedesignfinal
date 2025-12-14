using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class count_down : MonoBehaviour
{
    public TextMeshProUGUI countInText;
    public Light2D slothCenterLight;   
    public GameObject[]  eyegroup1;
    public GameObject[] eyegroup2;
    public AudioSource music;
    private bool started = false;
    //public TimeBar tb;

    
    void Start()
    {
        foreach (var g in eyegroup1)
        {
            g.SetActive(false);
        }
        foreach (var g in eyegroup2)
        {
            g.SetActive(false);
        }
        slothCenterLight.enabled = false;
    }

    public void StartCountdown()
    {
        if (started) return;
        started = true;
        StartCoroutine(Countdown("3", "2", "1", "GO!"));
    }

    IEnumerator Countdown (string s3, string s2, string s1, string go)
    {
        // interactables.SetActive (false);
        countInText.text = s3;
        SlothLight(3);
        yield return new WaitForSeconds (1.2f);

        countInText.text = s2;
        SlothLight(2);
        yield return new WaitForSeconds (1.2f);

        countInText.text = s1;
        SlothLight(1);
        yield return new WaitForSeconds (1.2f);

        countInText.text = go;
        yield return new WaitForSeconds (1.2f);
        countInText.text = "";
        countInText.enabled = false;

        music.Play();

    }

    void SlothLight(int stage)
    {
        if (stage == 3)
        {
            slothCenterLight.enabled = true;
        }

        if (stage == 2)
        {
            foreach (var g in eyegroup1)
            {
                g.SetActive(true);
            }
        }
        if (stage == 1)
        {
            foreach (var g in eyegroup2)
            {
                g.SetActive(true);
            }
        }
    }

}
