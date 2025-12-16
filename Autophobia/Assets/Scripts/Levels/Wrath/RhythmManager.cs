using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class RhythmManager : MonoBehaviour
{
    public SpriteRenderer handSprite;
    public Transform center;
    public Transform player;

    //public float bpm = 118f;
    public float bpm = 100f;
    public KnifeController[] knives;

    private float beatInterval;
    private float timer;
    private int beatCount = 0;
    private SpriteRenderer sr;
    private float offsetTime = 1.3f;
    private bool CRrunning = false;
    private KnifeController thisknife;

    private int nextIndex = 0; // for spawn time
    public List<float> spawnTimes = new List<float>(); //manually assiagned attack
    public float attackDelay = 1.2f;
    public AudioSource musicSource;
    public healthBar health;
    private bool musicStarted = false;

    private int beatKnifeIndex = 0; //for beat attack


    void Start()
    {
        beatInterval = 60f / bpm;
        StartCoroutine(WaitForMusicToStart());
    }

    IEnumerator WaitForMusicToStart()
    {
        while (musicSource.time <= 0.01f)
            yield return null;
        Play();
    }

    void Update()
    {
        checkMusicEnd();
        //make spawn time manually
        if (nextIndex >= spawnTimes.Count){
        return;
        }
        float targetTime = spawnTimes[nextIndex] - 0.3f;

        if (musicSource.time >= targetTime)
        {
            Debug.Log("trigger next knife");
            TriggerNextKnife();
            nextIndex++;
        }
        
        // //let it follow the beat
        // timer += Time.deltaTime;
        // if (timer >= beatInterval)
        // {
        //     timer -= beatInterval;
        //     //beatCounter++;
        //     TriggerBeatKnife();

        //     //HandleBeat(beatCount);
        // }
    
    }
    // void TriggerBeatKnife()
    // {
    //     if (knives.Length == 0) return;

    //     KnifeController knife = knives[beatKnifeIndex];

    //     // Beat: less damage than spawn time attack
    //     StartCoroutine(FlashThenAttack(knife));
    //     knife.TriggerBeatAttack();

    //     // follow the order of knifes
    //     beatKnifeIndex = (beatKnifeIndex + 1) % knives.Length;
    // }

    public void TriggerNextKnife()
    {
        int sector = GetPlayerSector();
        foreach (var knife in knives)
        {
            if (knife.sectorIndex == sector)
            {
                sr = knife.self.GetComponent<SpriteRenderer>();
                //StartCoroutine(FlashThenAttack(knife,sr));
                StartCoroutine(FlashThenAttack(knife));
                //thisknife = knife;
                // StartCoroutine (sequence (sr.color, Color.red));
                // StartColorLerp (sr.color, Color.red);
                // StartCoroutine (ColorFlash());
                
                //knife.TriggerAttack();
                
                return; 
            }
        }
    }
    //flash red color
    // IEnumerator FlashThenAttack(KnifeController knife, SpriteRenderer sr)
    // {
    //     Color original = sr.color;

    //     sr.color = Color.red;
    //     yield return new WaitForSeconds(0.3f);

    //     sr.color = original;

    //     knife.TriggerAttack();
    // }

    IEnumerator FlashThenAttack(KnifeController knife)
    {
        //light become brighter
        if (knife.knifeLight != null)
        {
            StartCoroutine(LightFlash(knife.knifeLight));
        }

        yield return new WaitForSeconds(0.3f);
        knife.TriggerAttack();
    }

    IEnumerator LightFlash(Light2D light)
    {
        float start = 0.7f;
        float end = 2.1f;
        float t = 0f;

        // brighter
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(start, end, t / 0.15f);
            yield return null;
        }

        // return back
        t = 0f;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(end, start, t / 0.15f);
            yield return null;
        }

        light.intensity = start;
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

        foreach (var k in knives)
        {
            k.canStart = true;
        }
    }
    
    void checkMusicEnd()
    {
        if (!musicStarted)
        {
            return;
        }    

        if (!musicSource.isPlaying) 
        {
            if (health.healthLeft() > 0 && musicSource.time >= musicSource.clip.length - 0.1f) 
            {
                levelTracker.wrathComplete = true;
                Debug.Log("end");
                UnityEngine.SceneManagement.SceneManager.LoadScene("wrath_end_dialogue");
            }
        }
    }
}

