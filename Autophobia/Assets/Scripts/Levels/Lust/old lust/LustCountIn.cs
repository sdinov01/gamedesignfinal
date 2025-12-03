using UnityEngine;
using TMPro;
using System.Collections;
public class LustCountIn : MonoBehaviour
{
    public GameObject[] toActivate;
    public TextMeshProUGUI countInText;
    public GameObject redboss;
    public GameObject purpleboss;
    public GameObject platforms;
    public ModularAudioHandler M;
    public GameObject activationGroup;
    public AudioSource music;
    public TimeBar tb;

    public bool IsLust;
    public GameObject bluelight;
    public GameObject whitelight;
    public GameObject redlight;
    public GameObject mainlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine (Countdown ("3", "2", "1", "GO!"));
    }

    IEnumerator Countdown (string s3, string s2, string s1, string go)
    {
        // interactables.SetActive (false);
        countInText.text = s3;
        EnableIfTrue (IsLust, bluelight);
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s2;
        EnableIfTrue (IsLust, whitelight);
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s1;
        EnableIfTrue (IsLust, redlight);
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = go;
        EnableIfTrue (IsLust, mainlight);
        yield return new WaitForSeconds ((float)M.beat2int);
        ActivateAll();
        // interactables.SetActive (true);

    }

    void EnableIfTrue (bool b, GameObject g)
    {
        if (b) { g.SetActive (true); }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ActivateAll()
    {
        for (int i = 0; i < toActivate.Length; i++)
        {
            Ainn (toActivate[i]);
        }

        // redboss.SetActive  (true);
        // platforms.SetActive (true);
        countInText.gameObject.SetActive (false);
        // Ainn (activationGroup);
        Ainn (purpleboss);
        Ainn (redboss);
        tb.SetDuration(music.clip.length);
        tb.BeginTime();
        music.Play();
        
    }

    private void Ainn (GameObject g)
    {
        if (g != null)
        {
            g.SetActive (true);
        }
    }
}
