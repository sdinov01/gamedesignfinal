using UnityEngine;

public class RhythmController : MonoBehaviour
{
    public float bpm = 120f;
    public KnifeController[] knives;

    private float beatInterval;
    private float timer;
    private int beatCount = 0;

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
        int beatInBar = ((beat - 1) % 4) + 1; // 1~4 循环

        if (beat <= 4)
        {
            // 前半段：只在第 4 拍刺
            if (beatInBar == 4)
                TriggerAllKnives();
        }
        else
        {
            // 后半段：第 2、4 拍刺
            if (beatInBar == 2 || beatInBar == 4)
                TriggerAllKnives();
        }
    }

    void TriggerAllKnives()
    {
        foreach (var knife in knives)
        {
            knife.TriggerAttack();
        }
    }
}