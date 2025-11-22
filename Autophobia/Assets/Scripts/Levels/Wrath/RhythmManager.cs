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

    //private int nextKnifeIndex = 0; //  当前轮到哪一把刀

    void Start()
    {
        beatInterval = 60f / bpm;
    }

    void Update()
    {
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
        int beatInBar = ((beat - 1) % 4) + 1; // 1~4 循环 4/4

        if (beat <= 4)
        {
            // if (beatInBar == 4)
            //     TriggerNextKnife();
        }
        else
        {
            // if (beatInBar == 2 || beatInBar == 4)
            //     TriggerNextKnife();
        }
    }

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

        float startAngle = 0f;
        float endAngle = 180f;

        int sectorCount = knives.Length;
        float sectorSize = (endAngle - startAngle) / sectorCount;

        int sector = Mathf.FloorToInt((angle - startAngle) / sectorSize);
        return Mathf.Clamp(sector, 0, sectorCount - 1);
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