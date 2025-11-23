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

    private int nextIndex = 0; // for spawn time
    public List<float> spawnTimes = new List<float>(); 
    public float attackDelay = 1.2f;
    public AudioSource musicSource;


    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
        if (nextIndex >= spawnTimes.Count)
        return;

        float targetTime = spawnTimes[nextIndex] + attackDelay;

        if (musicSource.time >= targetTime)
        {
            TriggerNextKnife();
            nextIndex++;
        }
        
        // timer += Time.deltaTime;
        // if (timer >= beatInterval)
        // {
        //     timer -= beatInterval;
        //     beatCount++;

        //     HandleBeat(beatCount);
        // }
    
    }

    // void HandleBeat(int beat)
    // {
    //     for (int i = 0; i < spawnTimes.Count; i++)
    //     {
    // int beatInBar = ((beat - 1) % 4) + 1; 

    // if (beat <= 4)
    // {
    //     if (beatInBar == 4)
    //         TriggerNextKnife();
    // }
    // else
    // {
    //     if (beatInBar == 2 || beatInBar == 4)
    //         TriggerNextKnife();
    // }
    // }

    public void TriggerNextKnife()
    {
        int sector = GetPlayerSector();
        foreach (var knife in knives)
        {
            if (knife.sectorIndex == sector)
            {
                StartCoroutine(ColorFlash());
                knife.TriggerAttack();
                return; 
            }
        }
    }

    int GetPlayerSector()
    {
        Vector2 dir = player.position - center.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // if (angle >180f) 
        //     return -1; 

        if (angle > 350f || angle < 20f) return 0;
        if (angle < 50f) return 1;
        if (angle < 90f) return 2;
        if (angle < 130f) return 3;
        if (angle < 170f) return 4;
        if (angle < 240f) return 5; 
        return 5;    // angle: 160–180
        
    }

    IEnumerator ColorFlash()
    {
        Color original = Color.white;

        Color flashColor;
        ColorUtility.TryParseHtmlString("#D2B1B1", out flashColor);

        handSprite.color = flashColor;

        yield return new WaitForSeconds(0.1f);

        handSprite.color = original;
    }
}