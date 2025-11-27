using UnityEngine;
using TMPro;
using System.Collections;
public class LustCountIn : MonoBehaviour
{
    public TextMeshProUGUI countInText;
    public GameObject redboss;
    public GameObject purpleboss;
    public GameObject audio;
    public GameObject platforms;
    public ModularAudioHandler M;
    public GameObject interactables;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine (Countdown ("3", "2", "1", "GO!"));
    }

    IEnumerator Countdown (string s3, string s2, string s1, string go)
    {
        // interactables.SetActive (false);
        countInText.text = s3;
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s2;
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s1;
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = go;
        yield return new WaitForSeconds ((float)M.beat2int);
        ActivateAll();
        countInText.gameObject.SetActive (false);
        // interactables.SetActive (true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ActivateAll()
    {
        // redboss.SetActive  (true);
        platforms.SetActive (true);
        purpleboss.SetActive (true);
        audio.SetActive (true);
        redboss.SetActive  (true);
        
    }
}
