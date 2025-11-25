using UnityEngine;
using TMPro;
using System.Collections;

public class CountIn : MonoBehaviour
{
    public TextMeshProUGUI countInText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stageScore;
    public TextMeshProUGUI targetText;
    public PlatformSpawner PS;
    public GameObject ball;
    public ModularAudioHandler M;
    public AudioSource music;
    public BeatSync BS;
    private int countdown = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine (Countdown ("3", "2", "1", "GO!"));
    }

    // Update is called once per frame
    IEnumerator Countdown (string s3, string s2, string s1, string go)
    {
        countInText.text = s3;
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s2;
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = s1;
        // ball.SetActive(true);
        yield return new WaitForSeconds ((float)M.beat2int);
        countInText.text = go;
        yield return new WaitForSeconds ((float)M.beat2int);
        ActivateAll();
        countInText.gameObject.SetActive (false);

    }

    void ActivateAll()
    {
        scoreText.gameObject.SetActive (true);
        stageScore.gameObject.SetActive (true);
        targetText.gameObject.SetActive (true);
        PS.gameObject.SetActive (true);
        BS.enabled = true;
        music.gameObject.SetActive (true);
    }
}
