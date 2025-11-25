using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour
{
    public SpriteRenderer handSprite;
    public Transform center;
    public Transform player;

    public float bpm = 118f;
    public KnifeController[] knives;

    private float beatInterval;
    private float timer;
    private int beatCount = 0;
    private SpriteRenderer sr;
    private float offsetTime = 1.3f;
    private bool CRrunning = false;
    private KnifeController thisknife;

    private int nextIndex = 0; // for spawn time
    public List<float> spawnTimes = new List<float>(); 
    public float attackDelay = 1.2f;
    public AudioSource musicSource;
    public healthBar health;
    private bool musicStarted = false;

    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
        checkMusicEnd();
        //make spawn time manually
        // if (nextIndex >= spawnTimes.Count)
        // return;

        // float targetTime = spawnTimes[nextIndex] - attackDelay;

        // if (musicSource.time >= targetTime)
        // {
        //     TriggerNextKnife();
        //     nextIndex++;
        // }
        
        //let it follow the beat
        timer += Time.deltaTime;
        if (timer >= beatInterval)
        {
            timer -= beatInterval;
            beatCount++;

            HandleBeat(beatCount);
        }
    
    }

    void HandleBeat(int beat)
    {
    //     for (int i = 0; i < spawnTimes.Count; i++)
    //     {
        int beatInBar = ((beat - 1) % 4) + 1; 

        if (beat <= 4)
        {
            if (beatInBar == 4)
                TriggerNextKnife();
        }
        else
        {
            if (beatInBar == 2 || beatInBar == 4)
                TriggerNextKnife();
        }
    }

    public void TriggerNextKnife()
    {
        int sector = GetPlayerSector();
        foreach (var knife in knives)
        {
            if (knife.sectorIndex == sector)
            {
                sr = knife.self.GetComponent<SpriteRenderer>();
                thisknife = knife;
                // StartCoroutine (sequence (sr.color, Color.red));
                // StartColorLerp (sr.color, Color.red);
                // StartCoroutine (ColorFlash());
                
                // knife.TriggerAttack();
                
                return; 
            }
        }
    }

    int GetPlayerSector()
    {
        Vector2 dir = player.position - center.position;
        float playerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (playerAngle < 0) playerAngle += 360f;

        float minDiff = 999f;
        int bestSector = -1;

        foreach (var knife in knives)
        {
            float diff = Mathf.Abs(Mathf.DeltaAngle(playerAngle, knife.angle));

            if (diff < minDiff)
            {
                minDiff = diff;
                bestSector = knife.sectorIndex;
            }
        }

        return bestSector;
    }


    // int GetPlayerSector()
    // {
    //     Vector2 dir = player.position - center.position;
    //     float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //     if (angle < 0) angle += 360f;

    //     // if (angle >180f) 
    //     //     return -1; 

    //     if (angle > 350f || angle < 20f) return 0;
    //     if (angle < 50f) return 1;
    //     if (angle < 90f) return 2;
    //     if (angle < 130f) return 3;
    //     if (angle < 170f) return 4;
    //     if (angle < 240f) return 5; 
    //     return 5;    // angle: 160–180

    // }

    // IEnumerator sequence (Color s, Color e)
    // {
    //     // yield return StartCoroutine (ColorLerp (s, e, offsetTime));
    //     yield return StartCoroutine (KnifeAttack (thisknife));
    //     // yield return StartCoroutine (ColorFlash ());
    // }

    // IEnumerator ColorFlash()
    // {
    //     Color original = Color.white;

    //     Color flashColor;
    //     ColorUtility.TryParseHtmlString("#D2B1B1", out flashColor);

    //     handSprite.color = flashColor;

    //     yield return new WaitForSeconds(0.1f);

    //     handSprite.color = original;
    // }

    // private IEnumerator KnifeAttack (KnifeController k)
    // {
    //     k.TriggerAttack();
    //     yield return null;
    // }

    // private IEnumerator ColorLerp (Color a, Color b, float duration)
    // {
    //     CRrunning = true;
    //     float t = 0f;
    //     while (t < duration)
    //     {
    //         t += Time.deltaTime;
    //         sr.color = Color.Lerp(a, b, t / duration);
    //         yield return null;
    //     }
    //     sr.color = a;  
    //     CRrunning = false;
    // }
    
    public void Play()
    {
        musicSource.Play();
        musicStarted = true;
    }
    
    void checkMusicEnd()
    {
        if (!musicStarted)
        {
            return;
        }    

        if (!musicSource.isPlaying) 
        {
            if (health.healthLeft() > 0) 
            {
                Debug.Log("end");
                UnityEngine.SceneManagement.SceneManager.LoadScene("wrath_end_dialogue");
            }
        }
    }
}

