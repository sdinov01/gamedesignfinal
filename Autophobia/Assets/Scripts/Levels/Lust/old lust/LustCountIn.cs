using UnityEngine;
using TMPro;
using System.Collections;
public class LustCountIn : MonoBehaviour
{
    public TextMeshProUGUI countInText;
    public GameObject redboss;
    public GameObject purpleboss;
    public GameObject platforms;
    public ModularAudioHandler M;
    public GameObject activationGroup;
    public AudioSource music;

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
        // redboss.SetActive  (true);
        // platforms.SetActive (true);
        countInText.gameObject.SetActive (false);
        if (activationGroup != null)
        {
            activationGroup.SetActive (true);
        }
        if (purpleboss != null)
        {
            purpleboss.SetActive (true);
        }
        if (redboss != null)
        {
            redboss.SetActive  (true);
        }
        music.Play();
    }
}
