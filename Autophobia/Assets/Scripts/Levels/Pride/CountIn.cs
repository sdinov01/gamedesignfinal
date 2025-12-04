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
    public Timer T;
    public SpriteRenderer prideart;
    public SpriteRenderer centercircle;
    public GameObject S0;

    public GameObject innerSpin;
    public GameObject buffer;
    public GameObject outerSpin;


    public linearPlatMove lpm;


    [SerializeField] private TimeBar timeBar;

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
        innerSpin.SetActive (true);
        countInText.text = s2;
        Instantiate (ball, S0.transform);
        yield return new WaitForSeconds ((float)M.beat2int);
        buffer.SetActive (true);
        countInText.text = s1;
        yield return new WaitForSeconds ((float)M.beat2int);
        outerSpin.SetActive (true);
        countInText.text = go;
        yield return new WaitForSeconds ((float)M.beat2int);

        if (lpm != null)
        {
            lpm.CountInComplete();
        }

        ActivateAll();
        countInText.gameObject.SetActive (false);
    }

    void ActivateAll()
    {
        scoreText.gameObject.SetActive (true);
        stageScore.gameObject.SetActive (true);
        targetText.gameObject.SetActive (true);
        PS.gameObject.SetActive (true);
        T.gameObject.SetActive (true);
        BS.enabled = true;
        music.gameObject.SetActive (true);
        prideart.enabled = true;
        centercircle.color = Color.white;

        timeBar.SetDuration(music.clip.length);
        timeBar.BeginTime();
    }
}
